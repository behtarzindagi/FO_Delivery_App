using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using ClosedXML.Excel;
using System.IO;
using System.Configuration;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using DataLayer;
using System.Reflection;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.html.simpleparser;
//using iTextSharp.tool.xml;
using iTextSharp.text.html;
using System.Web.Mvc;
using Entity;
using HiQPdf;
using Gma.QrCodeNet.Encoding;
using Gma.QrCodeNet.Encoding.Windows.Render;
using System.Drawing;
using System.Drawing.Imaging;

namespace Helper
{
    public class ReportInXMail
    {
        public static void SendMailForFoTripOrders(DataTable dataTable, string sendToList, string sendCCList, string mailSubject, string mailBody, string fileName, int userId)
        {
            try
            {
                //DataTable dataTable = gettab();
                DataSet ds = new DataSet();

                var dt = dataTable.Copy();
                dt.TableName = "Table1";
                ds.Tables.Add(dt);
                byte[] bytes = null;
                using (XLWorkbook wb = new XLWorkbook())
                {
                    wb.Worksheets.Add(ds);
                    wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                    wb.Style.Font.Bold = true;
                    //Response.Clear();
                    //Response.Buffer = true;
                    //Response.Charset = "";
                    //Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                    //Response.AddHeader("content-disposition", "attachment;filename=" + sheetName);
                    using (MemoryStream MyMemoryStream = new MemoryStream())
                    {
                        wb.SaveAs(MyMemoryStream);
                        //MyMemoryStream.WriteTo(Response.OutputStream);
                        //Response.Flush();
                        //Response.End();
                        bytes = MyMemoryStream.ToArray();
                        MyMemoryStream.Close();
                    }
                }
                // var toList = sendToList;
                //var ccList = sendCCList;
                var mailFrom = Convert.ToString(ConfigurationManager.AppSettings["UMail"]);
                var password = Convert.ToString(ConfigurationManager.AppSettings["UMailPass"]);
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["UMailFrom"]));

                var sendArrToList = sendToList.Split(',');
                var sendArrCCList = sendCCList.Split(',');

                //Start Test
                //mail.To.Add("arpit.jain@handygo.com");
                foreach (var toName in sendArrToList)
                {
                    if(toName!="")
                    mail.To.Add(toName);
                }
                //End
                foreach (var ccName in sendArrCCList)
                {
                    if (ccName != "")
                        mail.CC.Add(ccName);
                }

                mail.Subject = mailSubject;//sheetType.Subject + " " + fromOrderDate;

                mail.Body = mailBody;// string.Format(mailBody, "ext"); //string.Format(sheetType.Body, fromOrderDate);
                mail.IsBodyHtml = true;

                mail.Attachments.Add(new Attachment(new MemoryStream(bytes), fileName + ".xls"));
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                SmtpClient smtp = new SmtpClient
                {
                    Host = Convert.ToString(ConfigurationManager.AppSettings["UMailHost"]), // smtp server address here…
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["UMailPort"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(mailFrom, password),
                    Timeout = 30000,
                };
                smtp.TargetName = "STARTTLS/mail.handygo.com";
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);
                LogDal.MailLog(sendToList, sendCCList, mailSubject, mailBody, userId);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog("Helper/ReportInXMail", MethodBase.GetCurrentMethod().Name, ex.Message, userId);

            }

        }

        #region  PDF Generate For FO Trip
        /*Send PDF in Mail*/
        public static void SendMailForFoTripOrdersPDF(List<TripSheetModel> tripOrdersList, string sendToList, string sendCCList, string mailSubject, string mailBody, string fName, string FoName, string OrderDate, string District, int createdby)
        {
            try
            {
                // create the HTML to PDF converter
                HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

                // set browser width
                htmlToPdfConverter.BrowserWidth = int.Parse(ConfigurationManager.AppSettings["browserWidth"]);

                // set browser height if specified, otherwise use the default
                if (Convert.ToInt32(ConfigurationManager.AppSettings["browserHeight"]) > 0)
                    htmlToPdfConverter.BrowserHeight = int.Parse(ConfigurationManager.AppSettings["browserHeight"]);

                // set HTML Load timeout
                htmlToPdfConverter.HtmlLoadedTimeout = int.Parse("120");

                // set PDF page size and orientation
                htmlToPdfConverter.Document.PageSize = GetPageSize();
                string pageOrientation = ConfigurationManager.AppSettings["TripSheetPageOrientation"];
                htmlToPdfConverter.Document.PageOrientation = GetSelectedPageOrientation(pageOrientation);

                // set PDF page margins
                htmlToPdfConverter.Document.Margins = new PdfMargins(0);

                // set a wait time before starting the conversion
                htmlToPdfConverter.WaitBeforeConvert = int.Parse("2");

                // convert HTML to PDF
                byte[] pdfBuffer = null;


                //if (radioButtonConvertUrl.Checked)
                //{
                //    // convert URL to a PDF memory buffer
                //    string url = textBoxUrl.Text;

                //    pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
                //}mo
                //else
                //{
                //    // convert HTML code
                //    string htmlCode = textBoxHtmlCode.Text;
                //    string baseUrl = textBoxBaseUrl.Text;

                // convert HTML code to a PDF memory buffer
                //pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
                pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(GetTripSheetHTML(tripOrdersList, FoName, OrderDate, District), "www.goggle.co.in");

                var mailFrom = Convert.ToString(ConfigurationManager.AppSettings["UMail"]);
                var password = Convert.ToString(ConfigurationManager.AppSettings["UMailPass"]);
                var bccIds = Convert.ToString(ConfigurationManager.AppSettings["BTCottonBccIds"]);
                MailMessage mail = new MailMessage();
                Byte[] memoryStream = pdfBuffer;
                mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["UMailFrom"]));
                //var body = "<h3> PFA </h3>  <p><strong> This mail Contains as attachment - </strong></p>   <p style='text-indent: 25px;'> 1.FO order sheet of orders to be delivered on { 0}</p>   <p style='text-indent: 25px;'> 2.POD of orders to be delivered on { 0}</p>  <p style='margin-bottom:0; margin-top:50px'> Thanks </p>   <p style='margin-top:0'><strong> Behtar Zindagi Support</strong></p><p><strong> Note: This is an auto - generated mail.Please do not reply.</strong></p>";

                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                mail.Subject = mailSubject;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                SmtpClient smtp = new SmtpClient
                {
                    Host = Convert.ToString(ConfigurationManager.AppSettings["UMailHost"]), // smtp server address here…
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["UMailPort"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(mailFrom, password),
                    Timeout = 30000,
                };
                smtp.TargetName = "STARTTLS/mail.handygo.com";

                var toList = sendToList.Split(',');
                var bccList = "";
                var ccList = sendCCList.Split(',');

                mail.Bcc.Clear();
                //foreach (var toName in bccList)
                //{
                //    mail.Bcc.Add(toName);
                //}
                mail.To.Clear();
                foreach (var toName in toList)
                {
                    mail.To.Add(toName);
                }

                mail.CC.Clear();
                foreach (var toName in ccList)
                {
                    if(toName!="")
                    mail.CC.Add(toName);
                }

                mail.Attachments.Clear();
                var fileName = fName + "_TripSheet.pdf";
                Attachment pdfAttachment = new Attachment(new MemoryStream(memoryStream), fileName);
                pdfAttachment.ContentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                mail.Attachments.Add(pdfAttachment);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);


                // }

                //// inform the browser about the binary data format
                //HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

                //// let the browser know how to open the PDF document, attachment or inline, and the file name
                //bool attachment = true;
                //HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
                //    attachment ? "inline" : "attachment", pdfBuffer.Length.ToString()));

                //// write the PDF buffer to HTTP response
                //HttpContext.Current.Response.BinaryWrite(pdfBuffer);

                //// call End() method of HTTP response to stop ASP.NET page processing
                //HttpContext.Current.Response.End();
                LogDal.MailLog(sendToList, sendCCList, mailSubject, mailBody, createdby);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog("Helper/SendMailForFoTripOrdersPDF", MethodBase.GetCurrentMethod().Name, ex.Message, createdby);

            }
        }
        public static string GetTripSheetHTML(List<TripSheetModel> tripOrdersList, string FoName, string OrderDate,string District)
        {
            string temp = "1";
            string billHtml = string.Empty;
            //StringBuilder billHtml = new StringBuilder();
            billHtml += "<html>" +
                  "<head>" +
                     "<title></title>" +

                     "<meta charset = 'utf-8' />" +
                  "</head>" +
                  "<body>";
            billHtml += "<div style='font-family: 'Source Sans Pro',sans-serif!important; background-color:#fff; font-size: 11px;font-weight: 400; line-height: 1.42857143;     -webkit-font-smoothing: antialiased; color: #333;'>" +
       "<div style ='height:30px;background-color:#FF4500'></div>" +
                "<div style ='position: relative;  background: #fff;   border: 1px solid #f4f4f4;   padding: 20px;   margin: 10px 25px;'>" +


            "<div style = 'width:30%; float:left; '>" +


                 "<b> FO Name:</b> " + FoName + " <br>" +

                 "</div>" +

                 "<div style = 'width:30%; float:left;'>" +


                      "<b> Delivery Date:</b> " + OrderDate + " <br>" +

                    "</div>" +

                    "<div style = 'width:30%; float:left;'>" +


                      "<b> District:</b> " + District + " <br>" +

                    "</div>" +

                     "<div style = 'clear:both;'></div>" +

                     "<div style = 'width:100%; margin-bottom:10px; margin-top:0; height:10px; background-color:transparent;'></div>" +

                       "<div style = 'min-height: .01%; overflow-x: auto;'> ";

            billHtml += "<table style='width: 100%; border:1px solid #555; font-size:12px; max-width: 100%; margin-bottom: 5px; background-color: transparent; border-spacing: 0; border-collapse: collapse; display: table; table-layout:fixed; word-break:break-all;'>" +
                    "<thead style = 'display: table-header-group; vertical-align: middle; border-color: inherit;'>" +

                         "<tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +

                              "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:60px;' > Order No.</th>" +

                                   "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:2;width:190px;' > Product <br /> (Company) </th>" +
                                   "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:70px;' > Weight </ th>" +

                             "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:150px;' > Farmer Name <br /> (Father Name)</ th>" +

                                     "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:60px;' > Block </ th>" +

                                      "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:60px;' > Village </ th>" +

                                       "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:160px;' > Address </ th>" +

                                        "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:60px;' > Total <br /> Quantity </ th>" +

                                         "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:60px;' > Total <br /> Collection </ th>" +
                                         "<th style='border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;width:40px;'>Bill/<br />POD</ th> " +

                                          "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;;width:60px;' > Order <br /> status <br />  </ th>" +

                                               "<th style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px solid #555; margin:0; padding:0;;width:90px;' > Customer <br /> Signature </ th> ";

            billHtml += "</tr>" +
                    "</thead >" +
                    "<tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit; text-align:left; '>";
            foreach (var item in tripOrdersList)
            {
                billHtml += "<tr style ='background-color: #f9f9f9; display: table-row; vertical-align: inherit; border-color: inherit;'>" +

                     "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.OrderNo.ToString() + " </ td>" +

                      "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.Product + " </ td> " +
                    " <td style='margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>" + item.Weight.ToString() + "</td>" +
                   "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' >" + item.Farmer + " </ td>" +

                    "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.Block + " </ td>" +

                     "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.Village + " </ td>" +

                          "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.Address + "</ td>" +

                           "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.Qty.ToString() + "</ td>" +

                            "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.Amount.ToString() + " </ td>" +
                            "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' > " + item.InvoicePODStatus.ToString() + " </ td>" +

                            "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' >  </ td>" +

                            "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;' >  </ td>" +

                      " </ tr>";
            }
            billHtml += "</ tbody>" +
                "</table> ";

            billHtml += "</div> </div> <div style = 'clear:both'> </div>   </div> </body> </html> ";

            return billHtml;

        }
        private static PdfPageSize GetPageSize()
        {
            string pdfPageSize = "A4";
            pdfPageSize = ConfigurationManager.AppSettings["pdfPageSize"];
            switch (pdfPageSize)
            {
                case "A0":
                    return PdfPageSize.A0;
                case "A1":
                    return PdfPageSize.A1;
                case "A10":
                    return PdfPageSize.A10;
                case "A2":
                    return PdfPageSize.A2;
                case "A3":
                    return PdfPageSize.A3;
                case "A4":
                    return PdfPageSize.A4;
                case "A5":
                    return PdfPageSize.A5;
                case "A6":
                    return PdfPageSize.A6;
                case "A7":
                    return PdfPageSize.A7;
                case "A8":
                    return PdfPageSize.A8;
                case "A9":
                    return PdfPageSize.A9;
                case "ArchA":
                    return PdfPageSize.ArchA;
                case "ArchB":
                    return PdfPageSize.ArchB;
                case "ArchC":
                    return PdfPageSize.ArchC;
                case "ArchD":
                    return PdfPageSize.ArchD;
                case "ArchE":
                    return PdfPageSize.ArchE;
                case "B0":
                    return PdfPageSize.B0;
                case "B1":
                    return PdfPageSize.B1;
                case "B2":
                    return PdfPageSize.B2;
                case "B3":
                    return PdfPageSize.B3;
                case "B4":
                    return PdfPageSize.B4;
                case "B5":
                    return PdfPageSize.B5;
                case "Flsa":
                    return PdfPageSize.Flsa;
                case "HalfLetter":
                    return PdfPageSize.HalfLetter;
                case "Ledger":
                    return PdfPageSize.Ledger;
                case "Legal":
                    return PdfPageSize.Legal;
                case "Letter":
                    return PdfPageSize.Letter;
                case "Letter11x17":
                    return PdfPageSize.Letter11x17;
                case "Note":
                    return PdfPageSize.Note;
                default:
                    return PdfPageSize.A4;
            }
        }
        private static PdfPageOrientation GetSelectedPageOrientation(string pageOrientation)
        {
            //string pageOrientation = "P";

            return (pageOrientation == "P") ?
                PdfPageOrientation.Portrait : PdfPageOrientation.Landscape;

        }
        public static void SendMailForFoTripInvoice(List<PodDetailReport> GetInvoiceDetailReport, List<PodDetailReport> GetPodDetailReport, string sendToList, string sendCCList, string mailSubject, string mailBody, string fName, int createdby, string RecordId, string PDFname)
        {
            try
            {
                //  List<SP_PODDETAIL_v2_Result> sp_poddetail_v2_result = ClsHelper.GetPodData_V2("0", Convert.ToInt32(districtId), fromOrderDate, toOrderDate);
                // var sp_poddetail_v2_result = GetPodDetailReport;
                //foreach (var item in sp_poddetail_v2_result)
                //{
                //    item.QrCode = QRcodeImage(item.Invoice_No, item.FarmerRefNo, item.OrderRefNo);
                //}
                //return View(_vmbilldetail);

                // List<SP_PODDETAIL_Result> sp_poddetail_result = ClsHelper.GetPodData(Convert.ToString(0), districtID);
                // dName = dName.Contains(' ') ? dName.Replace(' ', '_') : dName;

                var pasgeSize = Convert.ToString(ConfigurationManager.AppSettings["pageSize"]);
                string strPOD = "";
                strPOD += "<html>" +
                "<head>" +
                 "<title></title>" +
                 "<meta charset = 'utf-8'/>" +
                "</head>" +
                "<body>";
                if (GetInvoiceDetailReport.Count > 0 && PDFname.Equals("Invoice_"))
                    strPOD = GetInvoiceHTML(GetInvoiceDetailReport, strPOD);
                if (GetPodDetailReport.Count > 0 && PDFname.Equals("POD_"))
                    strPOD = GetPODHTML(GetPodDetailReport, strPOD);

                strPOD += "</body>" +
                    "</html>";
                // create the HTML to PDF converter
                HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

                // set browser width
                htmlToPdfConverter.BrowserWidth = int.Parse(ConfigurationManager.AppSettings["browserWidth"]);

                // set browser height if specified, otherwise use the default
                if (Convert.ToInt32(ConfigurationManager.AppSettings["browserHeight"]) > 0)
                    htmlToPdfConverter.BrowserHeight = int.Parse(ConfigurationManager.AppSettings["browserHeight"]);

                // set HTML Load timeout
                htmlToPdfConverter.HtmlLoadedTimeout = int.Parse("120");

                // set PDF page size and orientation
                htmlToPdfConverter.Document.PageSize = GetPageSize();
                string pageOrientation = ConfigurationManager.AppSettings["InvoicePageOrientation"];
                htmlToPdfConverter.Document.PageOrientation = GetSelectedPageOrientation(pageOrientation);

                // set PDF page margins
                htmlToPdfConverter.Document.Margins = new PdfMargins(0);

                // set a wait time before starting the conversion
                htmlToPdfConverter.WaitBeforeConvert = int.Parse("2");

                // convert HTML to PDF
                byte[] pdfBuffer = null;


                pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(strPOD, "www.behtarzindagi.in");

                // pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(GetTripSheetHTML(tripOrdersList, FoName, OrderDate), "www.goggle.co.in");

                var mailFrom = Convert.ToString(ConfigurationManager.AppSettings["UMail"]);
                var password = Convert.ToString(ConfigurationManager.AppSettings["UMailPass"]);
                var bccIds = Convert.ToString(ConfigurationManager.AppSettings["BTCottonBccIds"]);
                MailMessage mail = new MailMessage();
                Byte[] memoryStream = pdfBuffer;
                mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["UMailFrom"]));
                //var body = "<h3> PFA </h3>  <p><strong> This mail Contains as attachment - </strong></p>   <p style='text-indent: 25px;'> 1.FO order sheet of orders to be delivered on { 0}</p>   <p style='text-indent: 25px;'> 2.POD of orders to be delivered on { 0}</p>  <p style='margin-bottom:0; margin-top:50px'> Thanks </p>   <p style='margin-top:0'><strong> Behtar Zindagi Support</strong></p><p><strong> Note: This is an auto - generated mail.Please do not reply.</strong></p>";

                mail.Body = mailBody;
                mail.IsBodyHtml = true;
                mail.Subject = mailSubject;
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                SmtpClient smtp = new SmtpClient
                {
                    Host = Convert.ToString(ConfigurationManager.AppSettings["UMailHost"]), // smtp server address here…
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["UMailPort"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(mailFrom, password),
                    Timeout = 30000,
                };
                smtp.TargetName = "STARTTLS/mail.handygo.com";

                var toList = sendToList.Split(',');
                var bccList = "";
                var ccList = sendCCList.Split(',');

                mail.Bcc.Clear();
                //foreach (var toName in bccList)
                //{
                //    mail.Bcc.Add(toName);
                //}
                mail.To.Clear();
                foreach (var toName in toList)
                {
                    mail.To.Add(toName);
                }

                mail.CC.Clear();
                foreach (var toName in ccList)
                {
                    if (toName != "")
                    mail.CC.Add(toName);
                }

                mail.Attachments.Clear();
                var fileName = PDFname + fName + ".pdf";
                Attachment pdfAttachment = new Attachment(new MemoryStream(memoryStream), fileName);
                pdfAttachment.ContentType.MediaType = System.Net.Mime.MediaTypeNames.Application.Pdf;
                mail.Attachments.Add(pdfAttachment);
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);

                ReasonStatusDal d = new ReasonStatusDal();
                d.InvoicePDFSendFlagUpdate(RecordId);

                LogDal.MailLog(sendToList, sendCCList, mailSubject, mailBody, createdby);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog("Helper/SendMailForFoTripInvoice", MethodBase.GetCurrentMethod().Name, ex.Message, createdby);

            }
            /*
            // }

            // inform the browser about the binary data format
            Response.AddHeader("Content-Type", "application/pdf");

            // let the browser know how to open the PDF document, attachment or inline, and the file name
            bool attachment = false;
            Response.AddHeader("Content-Disposition", String.Format("{0}; filename=" + dName + ".pdf; size={1}",
                attachment ? "inline" : "attachment", pdfBuffer.Length.ToString()));

            // write the PDF buffer to HTTP response
            Response.BinaryWrite(pdfBuffer);

            //// call End() method of HTTP response to stop ASP.NET page processing
            // Response.End();

            // Response.Write(pdfBuffer);
            //return RedirectToAction("GeneratePOD2", new { id = 0, msg = "" });

            return Content(pdfBuffer.ToString(), "application/pdf");*/

        }

        private static string GetInvoiceHTML(List<PodDetailReport> GetPodDetailReport, string billHtml)
        {
            //string billHtml = string.Empty;

            /*  billHtml += "<html>" +
                    "<head>" +
                     "<title></title>" +
                     "<meta charset = 'utf-8'/>" +
                    "</head>" +
                    "<body>";*/
            //" @{" +
            var invoiceList = GetPodDetailReport;
            var invoicenolist = invoiceList.GroupBy(x => x.Invoice_No).Select(y => y.First()).Select(fo => new _VmInvoiceOrder { InvoiceNo = fo.Invoice_No, OrderId = fo.OrderID }).ToList();
            foreach (var invc in invoicenolist)
            {

                var item = invoiceList.Where(l => l.Invoice_No == invc.InvoiceNo && l.OrderID == invc.OrderId).ToList();

                billHtml += " <div style = 'font-family:Source Sans Pro,sans-serif!important; background-color:#fff; font-size: 10px!important;   line-height: 1.42857143;     -webkit-font-smoothing: antialiased; color: #333;'>" +
                    "<div style ='height:40px;background-color:#FF4500'></div>" +
                " <div style = 'position:relative; background:#fff; border:1px solid #f4f4f4;'>" +


                      "<div style = 'width:70%; margin-bottom:10px;  float:left; '>" +

                          " <b style='font-size:17px'>" +
                              " Invoice for <strong> " +
                                 " " + item[0].FarmerRefNo.ToString() + "" +
                           "</strong>";

                DateTime dt = Convert.ToDateTime(item[0].DeliveryInstruction);
                string CreatedDate = dt.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                billHtml += CreatedDate.ToString() + " </b><br>" +


              "<b style = 'font-size:24px;'> Retail/TaxInvoice </b><br>" +



                       "<br/>" +



                      " <b style='font-size:17px'> Sold By </b><br/>" +

                      "<div style='font-size:17px'>" +

                         " <span>" +
                              item[0].DealerName + "<br/>" +
                             item[0].DealerAddress + "<br/>" +
                             ((string.IsNullOrEmpty(item[0].DealerVillageName) ? "" : (item[0].DealerVillageName + ','))) +
                            ((string.IsNullOrEmpty(item[0].DealerBlockName) ? "" : (item[0].DealerBlockName + ','))) +

                            // " <br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerDistrictName) ? "" : (item[0].DealerDistrictName + ','))) + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerStateName) ? "" : (item[0].DealerStateName + ','))) + " India" +
                          "</span>" +

                          "<br/><br/> " +

                            "<b> PAN Number:</b> " + item[0].PanNo.ToString() +
                             "<br/><b> GST Number:</b> " + item[0].GstNumber.ToString() + "<br>" +

                         " </div></div>" +

                          "<div style = 'width:30%; float:left; font-size:17px;'>";


                var QrCode = QRcodeImage(item[0].Invoice_No, item[0].FarmerRefNo, item[0].OrderRefNo);
                var base64 = Convert.ToBase64String(QrCode);
                var imgSrc = String.Format("data:image/png;base64,{0}", base64);

                billHtml += "<img src = " + imgSrc.ToString() + " style = 'margin:0; padding:0;' width='200' height='200'/>" +

                     
                   "</div>" +

                    "<div style = 'clear:both;'></div>" +

                    "<div style='width:100%; margin-bottom:10px;margin-top:10px;height:1px;background-color:#000;'></div>"+
              
                "<div style = 'font-size:17px;'>"+
 
                     "<div style = 'width:70%;float:left;font-size:17px;'>"+


                          "<b> Order ID:</b>" + item[0].OrderRefNo.Replace('/', '-') +"<br/>"+
     
                             "<b> Order Date:</b> "+item[0].CreatedDate.ToString("dd-MM-yyyy")+""+
                            "</div>"+
        
                            "<div style = 'width:30%; float:right; font-size:17px;'>"+


                                 "<b> Invoice No.:</b> " + item[0].Invoice_No +""+
            
                                    "<br/>"+
            
                                    "<b> Invoice Date:</b> "+item[0].DeliveryInstruction.ToString("dd-MM-yyyy")+
                                   "</div>"+
               

                               "</div>"+
               
                                   "<div style='clear:both;'></div> "+

                     "<div style = 'width:100%; margin-bottom:10px; margin-top:10px; height:1px; background-color:#000;'></div>" +

                     "<div style = 'width:70%; float:left; font-size:17px;'>" +

                          " <b> Billing Address </b><br/>" +


                              "<span>" +
                              (item[0].FName + ' ' + (string.IsNullOrEmpty(item[0].LName) ? "" : item[0].LName)) + " S/O " + item[0].FatherName + "<br/>" +
                              item[0].ShippingAddress + "<br/>" +
                              ((string.IsNullOrEmpty(item[0].VillageName) ? "" : (item[0].VillageName + ','))) +
                              ((string.IsNullOrEmpty(item[0].BlockName) ? "" : (item[0].BlockName + ','))) +

                              "<br/>" +
                              ((string.IsNullOrEmpty(item[0].DistrictName) ? "" : (item[0].DistrictName + ','))) + "<br/>" +
                              ((string.IsNullOrEmpty(item[0].StateName) ? "" : (item[0].StateName + ','))) + " India" +
                              "<br/><br/>" +
                              "Nature of Transaction:Sale" +
                          "</span>" +

                         "</div>" +


                         "<div style = 'width:30%; float:left; font-size:17px;'> ";





                billHtml += " <b> Shipping Address </b><br/>" +

                     "<span>" +
                           (item[0].FName + ' ' + (string.IsNullOrEmpty(item[0].LName) ? "" : item[0].LName)) + " S/O " + item[0].FatherName + "<br/>" +
                            item[0].ShippingAddress + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].VillageName) ? "" : (item[0].VillageName + ','))) +
                            ((string.IsNullOrEmpty(item[0].BlockName) ? "" : (item[0].BlockName + ','))) +

                            "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DistrictName) ? "" : (item[0].DistrictName + ','))) + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].StateName) ? "" : (item[0].StateName + ','))) + ", India" +
                        "</span>" +
                    "</div>" +
                    "<div style = 'clear:both;'></div><br/>" +


                     "<div style = 'overflow-x: auto;'>" +

                          "<table cellspacing = '0' cellpadding = '0' style = 'width: 100%; border:1px solid #555; font-size:15px; max-width: 100%;" +

    "margin-bottom: 20px; background-color: transparent; border-spacing: 0; border-collapse: collapse; display: table;'>" +
                                    " <thead style = 'display: table-header-group; vertical-align: middle; border-color: inherit;'> " +

                                          "<tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'> " +

                                               "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0; " +

    "padding: 5px; '>Sl No.</th>" +
                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0; " +

    "padding:5px; '>Description</th>" +
                                        "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding:5px;width:80px'>Unit Price<br/>(Rs.)</th>" +

                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding:5px; '>QTY</th>" +

                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;vertical-align:top; padding:5px;'><br/> Discount<br/>(Rs.) </th>" +


                                         "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding:5px;width:100px'>Net Price<br/>(Rs.)</th>" +
                                      "<th cellspacing = '0' cellpadding = '0' style = 'text-align:center; height:100%; border-right:1px " +

    "solid #555; margin:0; padding:5px'>CGST</th>" +
                                    "<th cellspacing = '0' cellpadding = '0' style = 'text-align:center;  height:100%; border-" +

    "right: 1px solid #555; margin:0; padding:5px'>SGST</th>" +
                                    "<th rowspan = '2' style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px " +

    "solid #555; margin:0; padding:5px;width:100px'>Net Amount<br/>(Rs.)</th>" +
                                "</tr> " +

                                "<tr>" +
                                    "<th>" +
                                        "<table width = '100%' cellspacing = '0' cellpadding = '0' style ='margin:0; padding:5px; font-size:15px; " +

    "border-collapse:collapse;'>" +

                                               "<tr>" +


                                                   "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse:collapse; text-align:center; margin: 0; padding: 5px; border-right:1px solid #555!important; border-top:1px solid " +

    "#555!important;'>Rate<br/>(%)</th>" +
                                                "<th style ='border:none!important; width:50%!important; border-" +

    "collapse:collapse; text-align:center; margin: 0; padding:5px; border-top:1px solid #555!important;'>Amount<br/>(Rs.)</th>" +
                                            "</tr> " +

                                        "</table>" +
                                    "</th> " +
                                    "<th>" +
                                        "<table width = '100%' cellspacing = '0' cellpadding = '0' style ='margin:0; padding:5px; font-size:15px; " +

    "border-collapse:collapse;'>" +
                                               "<tr>" +


                                                   "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse: collapse; text-align:center; margin: 0; padding:5px; border-right:1px solid #555!important; border-top:1px solid " +

    "#555!important; border-left:1px solid #555!important;'>Rate<br/>(%)</th>" +
                                                "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse: collapse; text-align:center; margin: 0; padding:5px; border-top:1px solid #555!important;'>Amount<br/>(Rs.)</th>" +
                                            "</tr> " +
                                        "</table>" +
                                    "</th> " +

                                "</tr>" +


                            "</thead>" +

                            "<tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit; text-" +

    "align: left; border-collapse:collapse; '>";

                var count = 1;

                decimal totalAmount = 0;
                decimal totalCgstAmount = 0;
                decimal totalSgstAmount = 0;
                decimal totalDiscountAmount = 0;
                decimal totalTax = 0;
                foreach (var iteml in item)
                {
                    totalDiscountAmount = totalDiscountAmount + iteml.DiscountedAmount;
                    totalAmount = totalAmount + iteml.TotalPayableAmount;
                    totalTax = iteml.Cgst + iteml.Sgst;
                    decimal baseAmount = (iteml.TotalPayableAmount * 100 / (100 + totalTax));
                    totalCgstAmount = (totalCgstAmount + Math.Round((iteml.Cgst > 0 ? (baseAmount * iteml.Cgst) / 100 : 0), 2));
                    totalSgstAmount = (totalSgstAmount + Math.Round((iteml.Cgst > 0 ? (baseAmount * iteml.Sgst) / 100 : 0), 2));

                    decimal unitPrice = (baseAmount / iteml.Quantity);

                    unitPrice = (iteml.DiscAmt + unitPrice);

                    billHtml += " <tr style ='background-color #f9f9f9; display table-row; vertical-align inherit; " +

"border-color inherit; '>" +
                                           "<td style = 'margin:0;padding:5px;text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + count.ToString() + "</td>" +
                                                 "<td style = 'margin:0; padding:5px;text-align:center;border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;text-align:left'>" + iteml.ProductName + "<br/>HSN Code : " +

iteml.HsnCode.ToString() + "</td> " +

                                        "<td style = 'margin:0; padding:5px; text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + unitPrice.ToString("0.00") + "</td>" +
                                                 "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + iteml.Quantity.ToString() + "</td>" +
                                                 "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + iteml.DiscAmt.ToString() + "</td>" +
                                                " <td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + (Math.Truncate(100 * baseAmount) / 100) + "</td>" +

                                                 "<td style = 'margin:0; padding:5px; height:100%; border:1px solid #555; line-height: " +

"1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>" +
                                                     "<table width = '100%' height = '100%' cellspacing = '0' cellpadding = '0' style = '" +

"margin: 0; padding:5px; border-collapse:collapse; font-size:15px; '>" +
                                                     "<tr> " +

                                                         "<td style = 'border:none!important;width:50%!important;border-" +

"collapse:collapse;text-align:center;margin:0;vertical-align:top;padding:5px;border-right:1px solid #555!important;'>" + iteml.Cgst.ToString() + "</td>" +
                                                    "<td style ='border:none!important;width:50%!important;border-" +

"collapse:collapse;text-align:center;margin:0;vertical-align:top;padding:5px;'>" + (iteml.Cgst > 0 ? (baseAmount * iteml.Cgst) / 100 : 0).ToString("0.00") + "</td>" +
                                               "</tr>" +
                                            "</table>" +
                                       "</td>" +

                                        "<td style ='margin:0; padding:5px; height:100%; border:1px solid #555; line-height: " +

"1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>" +
                                                     "<table width = '100%' height = '100%' cellspacing = '0' cellpadding = '0' style ='" +

"margin: 0; padding: 5px; border-collapse:collapse; font-size:15px;'>" +
                                                    " <tr>";

                    billHtml += "<td style='border:none!important; width:50%!important; border-" +

        "collapse: collapse; text-align:center; margin:0;vertical-align:top; padding:5px; border-right:1px solid #555!important;'>" + iteml.Sgst.ToString() + "</td>" +
                                                            "<td style = 'border:none!important; width:50%!important; border-" +

        "collapse: collapse; text-align:center;margin:0;vertical-align:top;padding:5px;'>" + (iteml.Sgst > 0 ? (baseAmount * iteml.Sgst) / 100 : 0).ToString("0.00") + "</td> " +
                                                        "</tr> " +
                                                    "</table> " +
                                                "</td> " +


                                                "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

        "height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>Rs. " + iteml.TotalPayableAmount.ToString() + "</td>" +
                                                     "</tr>";
                    count = count + 1;

                    //  }
                }
                billHtml += "</tbody>" +
              "</table>" +
          "</div" +

          "<div style = 'width:100%; margin:0 4% 0 0;'>" +
              "<div style='min-height: .01%; overflow-x: auto;'>" +
                "  <table style = 'width: 100%; max-width: 100%; margin-bottom: 20px; background-color: transparent;" +

    "border-spacing: 0; border-collapse: collapse; display: table; font-size:12px;'>" +
    "                           <tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit;'>" +
                         " <tr style='display: table-row; vertical-align: inherit; border-color: inherit;'>" +
                          "    <th style = 'border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:15px; '><b>Total Discount (Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; font-" +

    "size:15px; text-align: right; vertical-align: top;'><b>" + totalDiscountAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +


     " <tr style='display: table-row; vertical-align: inherit; border-color: inherit;'>" +
                          "    <th style = 'border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:15px; '><b>Total CGST (Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; font-" +

    "size:15px; text-align: right; vertical-align: top;'><b>" + totalCgstAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +



    "                              <tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +
    "                                 <th style='border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:15px;'><b>Total SGST (Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding:8px; line-height: 1.42857143; font-" +

    "size:15px; text-align: right; vertical-align: top;'><b>" + totalSgstAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +
    "                              <tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +
    "                                 <th style='border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:16px;'><b>Total (Payable " +

    "Amount Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; text-" +

    "align: right; vertical-align: top; font-size:16px;'><b>" + totalAmount.ToString() + "</b></td>" +
    "                               </tr>" +

    "<tr style='display: table - row; border - color:inherit;'>"+


                                         "<td colspan = '2' style = 'border-top:1px solid #f4f4f4; padding:2px; width:100%!important; line-height:1;  vertical-align:top; font-size:16px;'> Amount in words: <b> "+Helper.NumberToWords(Convert.ToInt64(totalAmount))+" Rupees </b></td>"+
               
                                               "</tr> "+
    "                          </tbody>" +
    "                     </table>" +
    "                </div>" +
    "           </div>" +

    "          <div style = 'width:100%; margin-bottom:10px; margin-top:0; height:1px; background-color:#000;'></div>" +
    "          <div style='margin-bottom:20px; float:left; width:60%;font-weight:normal; font-size:17px;'>" +

                                   "<div>"+
       
                                       "<ul>"+
       
                                           "<li> बिका हुआ माल वापस नहीं होगा।</li>"+
         
                                             "<li> ग्राहक की मांग के अनुसार दवाई व बीज दिया जाता है।</li>"+
           
                                               "<li> किसान से आग्रह किया जाता है कि दवाई व बीज खरीदने से पहले कम्पनी द्वारा दी गई दिशा निर्देश तथा वारंटी पढ़ लें और उसका पालन करें।</li>"+
             
                                                 "<li> कम्पनी द्वारा बीज व दवाई के उत्पादन में दुकानदार की कोई भूमिका नहीं है, हम केवल उत्पादन एवम निर्माता कम्पनी व किसान के बीच में मध्यस्थ का कार्य करते हैं।</li>"+
               
                                                   "<li> बीज व दवाई के अच्छे व बुरे परिणाम के लिए दुकानदार दोषी नहीं होगा।</li>"+
                                    
                                "</ul>"+


                    "</div>"+
                "</div>"+
         " <div style='width:40%; font-size:15px; margin-bottom:20px;margin-top:20px; float:left;'> " +
            "  <p style = 'margin:0; padding:0; text-align:center;'> For "+item[0].DealerName+"</p> " +
             "     <p style = 'margin:0; padding:0; text-align:center; height:100px;'>" +



                 " </p>" +

                  "<p style= 'margin:0;font-size:15px; padding:0; text-align:center;'>[Authorised Signatory] </p>" +

              "</div>" +

              "<div style= 'clear:both;'></div>" +


              "<div style= 'width:100%; margin-bottom:10px; margin-top:0; height:1px; background-color:#000;'></div>" +

              "<div style= 'width:70%; margin-bottom:20px;  float:left;font-size:15px;'>" +

                "  <span>" +

                 "     <b>" +
                  "        For any query, Please give a missed call on 7876400500." +
                  "</b>" +
              "</span>" +
          "</div>" +
          "<div style = 'width:30%; margin-bottom:20px; float:left;'>" +

                  "<table>" +

                   "   <tr>" +

                    "      <td> Purchase made On </td>" +
                     " <td><img src = 'http://www.behtarzindagi.in/AgriCrm/Content/Img/logo_destp.png'/></td>" +

                     " </tr>" +

                  "</table>" +

             " </div>" +

              "<div style= 'clear:both;'></div>" +

          "</div>" +

         "<div style= 'clear:both'></div " +

      "</div>" +

      "<p style= 'page-break-before: always;'></p>";
            }

            //    billHtml += "</body>" +
            //"</html>";

            return billHtml;
        }

        private static string GetPODHTML(List<PodDetailReport> GetPodDetailReport, string billHtml)
        {
            //string billHtml = string.Empty;

            /*  billHtml += "<html>" +
                    "<head>" +
                     "<title></title>" +
                     "<meta charset = 'utf-8'/>" +
                    "</head>" +
                    "<body>";*/
            //" @{" +
            var invoiceList = GetPodDetailReport;
            var invoicenolist = invoiceList.GroupBy(x => new { x.DealerID, x.OrderID }).Select(y => y.First()).Select(fo => new _VmInvoiceOrder { DealerId = fo.DealerID, OrderId = fo.OrderID }).ToList();
            foreach (var invc in invoicenolist)
            {
                var item = invoiceList.Where(l => l.DealerID == invc.DealerId && l.OrderID == invc.OrderId).ToList();

                billHtml += " <div style = 'font-family:Source Sans Pro,sans-serif!important; background-color:#fff; font-size: 10px!important;   line-height: 1.42857143;     -webkit-font-smoothing: antialiased; color: #333;'>" +
                    "<div style ='height:40px;background-color:#FF4500'></div>" +
                " <div style = 'position:relative; background:#fff; border:1px solid #f4f4f4;'>" +


                      "<div style = 'width:70%; margin-bottom:10px;  float:left; '>" +

                          " <b style='font-size:17px'>" +
                              " POD for <strong> " +
                                 " " + item[0].FarmerRefNo.ToString() + "" +
                           "</strong>";

                DateTime dt = Convert.ToDateTime(item[0].DeliveryInstruction);
                string CreatedDate = dt.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                billHtml += CreatedDate.ToString() + " </b><br/>" +


              "<b style = 'font-size:24px;'> Delivery Book(POD) </b><br/>" +



                     /*  "<br/>" +



                      " <b style='font-size:17px'> Sold By </b><br/>" +

                      "<div style='font-size:17px'>" +

                         " <span>" +
                              item[0].DealerName + "<br/>" +
                             item[0].DealerAddress + "<br/>" +
                             ((string.IsNullOrEmpty(item[0].DealerVillageName) ? "" : (item[0].DealerVillageName + ','))) +
                            ((string.IsNullOrEmpty(item[0].DealerBlockName) ? "" : (item[0].DealerBlockName + ','))) +

                            // " <br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerDistrictName) ? "" : (item[0].DealerDistrictName + ','))) + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerStateName) ? "" : (item[0].DealerStateName + ','))) + " India" +
                          "</span>" +

                         // "<br/><br/> " +

                            //"<b> PAN Number:</b> " + item[0].PanNo.ToString() +
                            // "<br/><b> GST Number:</b> " + item[0].GstNumber.ToString() + "<br>" +

                         " </div>"+
                         */
                         "</div>" +

                          "<div style = 'width:30%; float:left; font-size:17px;'>";


                var QrCode = QRcodeImage("", item[0].FarmerRefNo, item[0].OrderRefNo);
                var base64 = Convert.ToBase64String(QrCode);
                var imgSrc = String.Format("data:image/png;base64,{0}", base64);

                billHtml += "<img src = " + imgSrc.ToString() + " style = 'margin:0; padding:0;' width='200' height='200'/>" +


                   "</div>" +

                    "<div style = 'clear:both;'></div>" +

                    "<div style='width:100%; margin-bottom:10px;margin-top:10px;height:1px;background-color:#000;'></div>" +

                "<div style = 'font-size:17px;'>" +

                     "<div style = 'width:70%;float:left;font-size:17px;'>" +


                          "<b> Order ID:</b>" + item[0].OrderRefNo.Replace('/', '-') + "<br/>" +

                             "<b> Order Date:</b> " + item[0].CreatedDate.ToString("dd-MM-yyyy") + "" +
                            "</div>" +

                            "<div style = 'width:30%; float:right; font-size:17px;'>" +


                                // "<b> Invoice No.:</b> " + item[0].Invoice_No + "" +

                                    "<br/>" +

                                    "<b> Delivery Date:</b> " + item[0].DeliveryInstruction.ToString("dd-MM-yyyy") +
                                   "</div>" +


                               "</div>" +

                                   "<div style='clear:both;'></div> " +

                     "<div style = 'width:100%; margin-bottom:10px; margin-top:10px; height:1px; background-color:#000;'></div>" +

                     "<div style = 'width:70%; float:left; font-size:17px;'>" +

                          " <b> Billing Address </b><br/>" +


                              "<span>" +
                              (item[0].FName + ' ' + (string.IsNullOrEmpty(item[0].LName) ? "" : item[0].LName)) + " S/O " + item[0].FatherName + "<br/>" +
                              item[0].ShippingAddress + "<br/>" +
                              ((string.IsNullOrEmpty(item[0].VillageName) ? "" : (item[0].VillageName + ','))) +
                              ((string.IsNullOrEmpty(item[0].BlockName) ? "" : (item[0].BlockName + ','))) +

                              "<br/>" +
                              ((string.IsNullOrEmpty(item[0].DistrictName) ? "" : (item[0].DistrictName + ','))) + "<br/>" +
                              ((string.IsNullOrEmpty(item[0].StateName) ? "" : (item[0].StateName + ','))) + " India" +
                              "<br/><br/>" +
                             // "Nature of Transaction:Sale" +
                          "</span>" +

                         "</div>" +


                         "<div style = 'width:30%; float:left; font-size:17px;'> ";





                billHtml += " <b> Shipping Address </b><br/>" +

                     "<span>" +
                           (item[0].FName + ' ' + (string.IsNullOrEmpty(item[0].LName) ? "" : item[0].LName)) + " S/O " + item[0].FatherName + "<br/>" +
                            item[0].ShippingAddress + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].VillageName) ? "" : (item[0].VillageName + ','))) +
                            ((string.IsNullOrEmpty(item[0].BlockName) ? "" : (item[0].BlockName + ','))) +

                            "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DistrictName) ? "" : (item[0].DistrictName + ','))) + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].StateName) ? "" : (item[0].StateName + ','))) + ", India" +
                        "</span>" +
                    "</div>" +
                    "<div style = 'clear:both;'></div><br/>" +


                     "<div style = 'overflow-x: auto;'>" +

                          "<table cellspacing = '0' cellpadding = '0' style = 'width: 100%; border:1px solid #555; font-size:15px; max-width: 100%;" +

    "margin-bottom: 20px; background-color: transparent; border-spacing: 0; border-collapse: collapse; display: table;'>" +
                                    " <thead style = 'display: table-header-group; vertical-align: middle; border-color: inherit;'> " +

                                          "<tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'> " +

                                               "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0; " +

    "padding: 5px; '>Sl No.</th>" +
                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0; " +

    "padding:5px; '>Description</th>" +
                                        "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding:5px;width:80px'>Unit Price<br/>(Rs.)</th>" +

                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding:5px; '>QTY</th>" +

                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;vertical-align:top; padding:5px;'><br/> Discount<br/>(Rs.) </th>" +


                                         "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding:5px;width:100px'>Net Price<br/>(Rs.)</th>" +
                                      "<th cellspacing = '0' cellpadding = '0' style = 'text-align:center; height:100%; border-right:1px " +

    "solid #555; margin:0; padding:5px'>CGST</th>" +
                                    "<th cellspacing = '0' cellpadding = '0' style = 'text-align:center;  height:100%; border-" +

    "right: 1px solid #555; margin:0; padding:5px'>SGST</th>" +
                                    "<th rowspan = '2' style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px " +

    "solid #555; margin:0; padding:5px;width:100px'>Net Amount<br/>(Rs.)</th>" +
                                "</tr> " +

                                "<tr>" +
                                    "<th>" +
                                        "<table width = '100%' cellspacing = '0' cellpadding = '0' style ='margin:0; padding:5px; font-size:15px; " +

    "border-collapse:collapse;'>" +

                                               "<tr>" +


                                                   "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse:collapse; text-align:center; margin: 0; padding: 5px; border-right:1px solid #555!important; border-top:1px solid " +

    "#555!important;'>Rate<br/>(%)</th>" +
                                                "<th style ='border:none!important; width:50%!important; border-" +

    "collapse:collapse; text-align:center; margin: 0; padding:5px; border-top:1px solid #555!important;'>Amount<br/>(Rs.)</th>" +
                                            "</tr> " +

                                        "</table>" +
                                    "</th> " +
                                    "<th>" +
                                        "<table width = '100%' cellspacing = '0' cellpadding = '0' style ='margin:0; padding:5px; font-size:15px; " +

    "border-collapse:collapse;'>" +
                                               "<tr>" +


                                                   "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse: collapse; text-align:center; margin: 0; padding:5px; border-right:1px solid #555!important; border-top:1px solid " +

    "#555!important; border-left:1px solid #555!important;'>Rate<br/>(%)</th>" +
                                                "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse: collapse; text-align:center; margin: 0; padding:5px; border-top:1px solid #555!important;'>Amount<br/>(Rs.)</th>" +
                                            "</tr> " +
                                        "</table>" +
                                    "</th> " +

                                "</tr>" +


                            "</thead>" +

                            "<tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit; text-" +

    "align: left; border-collapse:collapse; '>";

                var count = 1;

                decimal totalAmount = 0;
                decimal totalCgstAmount = 0;
                decimal totalSgstAmount = 0;
                decimal totalDiscountAmount = 0;
                decimal totalTax = 0;
                foreach (var iteml in item)
                {
                    totalDiscountAmount = totalDiscountAmount + iteml.DiscountedAmount;
                    totalAmount = totalAmount + iteml.TotalPayableAmount;
                    totalTax = iteml.Cgst + iteml.Sgst;
                    decimal baseAmount = (iteml.TotalPayableAmount * 100 / (100 + totalTax));
                    totalCgstAmount = (totalCgstAmount + Math.Round((iteml.Cgst > 0 ? (baseAmount * iteml.Cgst) / 100 : 0), 2));
                    totalSgstAmount = (totalSgstAmount + Math.Round((iteml.Cgst > 0 ? (baseAmount * iteml.Sgst) / 100 : 0), 2));

                    decimal unitPrice = (baseAmount / iteml.Quantity);

                    unitPrice = (iteml.DiscAmt + unitPrice);

                    billHtml += " <tr style ='background-color #f9f9f9; display table-row; vertical-align inherit; " +

"border-color inherit; '>" +
                                           "<td style = 'margin:0;padding:5px;text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + count.ToString() + "</td>" +
                                                 "<td style = 'margin:0; padding:5px;text-align:center;border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;text-align:left'>" + iteml.ProductName + "<br/>HSN Code : " +

iteml.HsnCode.ToString() + "</td> " +

                                        "<td style = 'margin:0; padding:5px; text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + unitPrice.ToString("0.00") + "</td>" +
                                                 "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + iteml.Quantity.ToString() + "</td>" +
                                                 "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + iteml.DiscAmt.ToString() + "</td>" +
                                                " <td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + (Math.Truncate(100 * baseAmount) / 100) + "</td>" +

                                                 "<td style = 'margin:0; padding:5px; height:100%; border:1px solid #555; line-height: " +

"1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>" +
                                                     "<table width = '100%' height = '100%' cellspacing = '0' cellpadding = '0' style = '" +

"margin: 0; padding:5px; border-collapse:collapse; font-size:15px; '>" +
                                                     "<tr> " +

                                                         "<td style = 'border:none!important;width:50%!important;border-" +

"collapse:collapse;text-align:center;margin:0;vertical-align:top;padding:5px;border-right:1px solid #555!important;'>" + iteml.Cgst.ToString() + "</td>" +
                                                    "<td style ='border:none!important;width:50%!important;border-" +

"collapse:collapse;text-align:center;margin:0;vertical-align:top;padding:5px;'>" + (iteml.Cgst > 0 ? (baseAmount * iteml.Cgst) / 100 : 0).ToString("0.00") + "</td>" +
                                               "</tr>" +
                                            "</table>" +
                                       "</td>" +

                                        "<td style ='margin:0; padding:5px; height:100%; border:1px solid #555; line-height: " +

"1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>" +
                                                     "<table width = '100%' height = '100%' cellspacing = '0' cellpadding = '0' style ='" +

"margin: 0; padding: 5px; border-collapse:collapse; font-size:15px;'>" +
                                                    " <tr>";

                    billHtml += "<td style='border:none!important; width:50%!important; border-" +

        "collapse: collapse; text-align:center; margin:0;vertical-align:top; padding:5px; border-right:1px solid #555!important;'>" + iteml.Sgst.ToString() + "</td>" +
                                                            "<td style = 'border:none!important; width:50%!important; border-" +

        "collapse: collapse; text-align:center;margin:0;vertical-align:top;padding:5px;'>" + (iteml.Sgst > 0 ? (baseAmount * iteml.Sgst) / 100 : 0).ToString("0.00") + "</td> " +
                                                        "</tr> " +
                                                    "</table> " +
                                                "</td> " +


                                                "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #555; line-" +

        "height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>Rs. " + iteml.TotalPayableAmount.ToString() + "</td>" +
                                                     "</tr>";
                    count = count + 1;

                    //  }
                }
                billHtml += "</tbody>" +
              "</table>" +
          "</div" +

          "<div style = 'width:100%; margin:0 4% 0 0;'>" +
              "<div style='min-height: .01%; overflow-x: auto;'>" +
                "  <table style = 'width: 100%; max-width: 100%; margin-bottom: 20px; background-color: transparent;" +

    "border-spacing: 0; border-collapse: collapse; display: table; font-size:12px;'>" +
    "                           <tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit;'>" +
                         " <tr style='display: table-row; vertical-align: inherit; border-color: inherit;'>" +
                          "    <th style = 'border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:15px; '><b>Total Discount (Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; font-" +

    "size:15px; text-align: right; vertical-align: top;'><b>" + totalDiscountAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +


     " <tr style='display: table-row; vertical-align: inherit; border-color: inherit;'>" +
                          "    <th style = 'border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:15px; '><b>Total CGST (Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; font-" +

    "size:15px; text-align: right; vertical-align: top;'><b>" + totalCgstAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +



    "                              <tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +
    "                                 <th style='border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:15px;'><b>Total SGST (Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding:8px; line-height: 1.42857143; font-" +

    "size:15px; text-align: right; vertical-align: top;'><b>" + totalSgstAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +
    "                              <tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +
    "                                 <th style='border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:16px;'><b>Total (Payable " +

    "Amount Rs.):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; text-" +

    "align: right; vertical-align: top; font-size:16px;'><b>" + totalAmount.ToString() + "</b></td>" +
    "                               </tr>" +

    "<tr style='display: table - row; border - color:inherit;'>" +


                                         "<td colspan = '2' style = 'border-top:1px solid #f4f4f4; padding:2px; width:100%!important; line-height:1;  vertical-align:top; font-size:16px;'> Amount in words: <b> " + Helper.NumberToWords(Convert.ToInt64(totalAmount)) + " Rupees </b></td>" +

                                               "</tr> " +
    "                          </tbody>" +
    "                     </table>" +
    "                </div>" +
    "           </div>" +

    "          <div style = 'width:100%; margin-bottom:10px; margin-top:0; height:1px; background-color:#000;'></div>" +
    "          <div style='margin-bottom:20px; float:left; width:60%;font-weight:normal; font-size:17px;'>" +

                                   "<span>"+
                            "<img src ='http://behtarzindagi.in/AgriCrm/Content/ImgDealer/pod_tippani.png'/>"+
 
                         "</span>"+
                 "</div>" +
       /*  " <div style='width:40%; font-size:15px; margin-bottom:20px;margin-top:20px; float:left;'> " +
            "  <p style = 'margin:0; padding:0; text-align:center;'> For " + item[0].DealerName + "</p> " +
             "     <p style = 'margin:0; padding:0; text-align:center; height:100px;'>" +



                 " </p>" +

                  "<p style= 'margin:0;font-size:15px; padding:0; text-align:center;'>[Authorised Signatory] </p>" +

              "</div>" +
             */
              "<div style= 'clear:both;'></div>" +


              "<div style= 'width:100%; margin-bottom:10px; margin-top:0; height:1px; background-color:#000;'></div>" +

              "<div style= 'width:70%; margin-bottom:20px;  float:left;font-size:15px;'>" +

                "  <span>" +

                 "     <b>" +
                  "        For any query, Please give a missed call on 7876400500." +
                  "</b>" +
              "</span>" +
          "</div>" +
          "<div style = 'width:30%; margin-bottom:20px; float:left;'>" +

                  "<table>" +

                   "   <tr>" +

                    "      <td> Purchase made On </td>" +
                     " <td><img src = 'http://www.behtarzindagi.in/AgriCrm/Content/Img/logo_destp.png'/></td>" +

                     " </tr>" +

                  "</table>" +

             " </div>" +

              "<div style= 'clear:both;'></div>" +

          "</div>" +

         "<div style= 'clear:both'></div " +

      "</div>" +

      "<p style= 'page-break-before: always;'></p>";
            }

            //    billHtml += "</body>" +
            //"</html>";

            return billHtml;
        }

        private static string GetPODHTML1(List<PodDetailReport> GetPodDetailReport, string billHtml)
        {
            /* string billHtml = string.Empty;

             billHtml += "<html>" +
   "<head>" +
     "<title></title>" +
     "<meta charset = 'utf-8'/>" +
   "</head>" +
   "<body>";*/
            //" @{" +
            var invoiceList = GetPodDetailReport;
            var invoicenolist = invoiceList.GroupBy(x => x.Invoice_No).Select(y => y.First()).Select(fo => new _VmInvoiceOrder { InvoiceNo = fo.Invoice_No, OrderId = fo.OrderID }).ToList();
            foreach (var invc in invoicenolist)
            {

                var item = invoiceList.Where(l => l.Invoice_No == invc.InvoiceNo && l.OrderID == invc.OrderId).ToList();

                billHtml += " <div style = 'font-family:Source Sans Pro,sans-serif!important; background-color:#fff; font-size: 10px!important;   line-height: 1.42857143;     -webkit-font-smoothing: antialiased; color: #333;'>" +

                " <div style = 'position:relative; background:#fff; border:1px solid #f4f4f4;'>" +


                      "<div style = 'width:70%; margin-bottom:10px;  float:left; '>" +

                          " <b>" +
                              " POD for <strong> " +
                                 " " + item[0].FarmerRefNo.ToString() + "" +
                           "</strong>";

                DateTime dt = Convert.ToDateTime(item[0].DeliveryInstruction);
                string CreatedDate = dt.ToString("dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture);


                billHtml += CreatedDate.ToString() + " </b><br>" +


              "<b style = 'font-size:17px;'> Delivery Book POD </b><br>" +



                       "<br/>" +



                      " <b> Sold By </b><br/>" +



                         " <span>" +
                              item[0].DealerName + "<br/>" +
                             item[0].DealerAddress + "<br/>" +
                             ((string.IsNullOrEmpty(item[0].DealerVillageName) ? "" : (item[0].DealerVillageName + ','))) +
                            ((string.IsNullOrEmpty(item[0].DealerBlockName) ? "" : (item[0].DealerBlockName + ','))) +

                             " <br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerDistrictName) ? "" : (item[0].DealerDistrictName + ','))) + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerStateName) ? "" : (item[0].DealerStateName + ','))) + ",India" +
                          "</span>" +

                         /*"<br/><br/>" +

                         "<b> GST Number:</b> " + item[0].GstNumber.ToString() + "<br>" +

                           "<b> PAN Number:</b> " + item[0].PanNo.ToString() +*/

                         " </div>" +

                          "<div style = 'width:30%; float:left;'>";


                var QrCode = QRcodeImage("", item[0].FarmerRefNo, item[0].OrderRefNo);
                var base64 = Convert.ToBase64String(QrCode);
                var imgSrc = String.Format("data:image/png;base64,{0}", base64);

                billHtml += "<img src = " + imgSrc.ToString() + " style = 'margin:0; padding:0;' width='150' height='150'/>" +

                   /* "<br/><b> Invoice No.:</b> " + item[0].Invoice_No.ToString() +*/
                   " </div>" +

                    "<div style = 'clear:both;'></div>" +

                     "<div style = 'width:100%; margin-bottom:10px; margin-top:10px; height:1px; background-color:#000;'></div>" +

                     " <div style = 'width:70%; float:left; '>" +

                          " <b> Billing Address </b><br/>" +


                              "<span>" +
                              (item[0].FName + ' ' + (string.IsNullOrEmpty(item[0].LName) ? "" : item[0].LName)) + "<br/>" +
                              item[0].ShippingAddress + "<br/>" +
                              ((string.IsNullOrEmpty(item[0].DealerVillageName) ? "" : (item[0].DealerVillageName + ','))) +
                              ((string.IsNullOrEmpty(item[0].DealerBlockName) ? "" : (item[0].DealerBlockName + ','))) +

                              "<br/>" +
                              ((string.IsNullOrEmpty(item[0].DealerDistrictName) ? "" : (item[0].DealerDistrictName + ','))) + "<br/>" +
                              ((string.IsNullOrEmpty(item[0].DealerStateName) ? "" : (item[0].DealerStateName + ','))) + ", India" +
                              "<br/><br/>" +
                              "Nature of Transaction:Sale" +
                          "</span>" +

                          "<br/><br/>" +

                          "<b> Order ID:</b> " + item[0].OrderRefNo + " <br>" +

                         "</div>" +


                         "<div style = 'width:30%; float:left;'> ";





                billHtml += " <b> Shipping Address </b><br/>" +

                     "<span>" +
                           (item[0].FName + ' ' + (string.IsNullOrEmpty(item[0].LName) ? "" : item[0].LName)) + "<br/>" +
                            item[0].ShippingAddress + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerVillageName) ? "" : (item[0].DealerVillageName + ','))) +
                            ((string.IsNullOrEmpty(item[0].DealerBlockName) ? "" : (item[0].DealerBlockName + ','))) +

                            "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerDistrictName) ? "" : (item[0].DealerDistrictName + ','))) + "<br/>" +
                            ((string.IsNullOrEmpty(item[0].DealerStateName) ? "" : (item[0].DealerStateName + ','))) + ", India" +
                        "</span>" +
                    "</div>" +
                    "<div style = 'clear:both;'></div>" +


                     "<div style = 'overflow-x: auto;'>" +

                          "<table cellspacing = '0' cellpadding = '0' style = 'width: 100%; border:1px solid #555; font-size:12px; max-width: 100%;" +

    "margin-bottom: 20px; background-color: transparent; border-spacing: 0; border-collapse: collapse; display: table;'>" +
                                    " <thead style = 'display: table-header-group; vertical-align: middle; border-color: inherit;'> " +

                                          "<tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'> " +

                                               "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0; " +

    "padding: 0; '>Sr.No.</th>" +
                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0; " +

    "padding: 0; '>Description</th>" +

                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding: 0; '>QTY</th>" +
                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding: 0; '>Amount<br/>Per UNIT(In Rs.)</th>" +
                                      "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0; padding:0;'><br/> Discount </th>" +


                                         "<th rowspan = '2' style = 'text-align:center; border:1px solid #555; margin:0;" +

    "padding: 0; '>Gross Amount<br/>(In Rs.)</th>" +
                                      "<th cellspacing = '0' cellpadding = '0' style = 'text-align:center; height:100%; border-right:1px " +

    "solid #555; margin:0; padding:0'>CGST</th>" +
                                    "<th cellspacing = '0' cellpadding = '0' style = 'text-align:center;  height:100%; border-" +

    "right: 1px solid #555; margin:0; padding:0'>SGST</th>" +
                                    "<th rowspan = '2' style = 'border-bottom: 2px solid #f4f4f4; text-align:center; border:1px " +

    "solid #555; margin:0; padding:0; '>Net Amount</th>" +
                                "</tr> " +

                                "<tr>" +
                                    "<th>" +
                                        "<table width = '100%' cellspacing = '0' cellpadding = '0' style ='margin:0; padding:0; font-size:12px; " +

    "border-collapse:collapse;'>" +

                                               "<tr>" +


                                                   "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse:collapse; text-align:center; margin: 0; padding: 0; border-right:1px solid #555!important; border-top:1px solid " +

    "#555!important;'>Rate</th>" +
                                                "<th style ='border:none!important; width:50%!important; border-" +

    "collapse:collapse; text-align:center; margin: 0; padding: 0; border-top:1px solid #555!important;'>Amount</th>" +
                                            "</tr> " +

                                        "</table>" +
                                    "</th> " +
                                    "<th>" +
                                        "<table width = '100%' cellspacing = '0' cellpadding = '0' style ='margin:0; padding:0; font-size:12px; " +

    "border-collapse:collapse;'>" +
                                               "<tr>" +


                                                   "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse: collapse; text-align:center; margin: 0; padding: 0; border-right:1px solid #555!important; border-top:1px solid " +

    "#555!important; border-left:1px solid #555!important;'>Rate</th>" +
                                                "<th style = 'border:none!important; width:50%!important; border-" +

    "collapse: collapse; text-align:center; margin: 0; padding: 0; border-top:1px solid #555!important;'>Amount</th>" +
                                            "</tr> " +
                                        "</table>" +
                                    "</th> " +

                                "</tr>" +


                            "</thead>" +

                            "<tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit; text-" +

    "align: left; border-collapse:collapse; '>";

                var count = 1;

                decimal totalAmount = 0;
                decimal totalCgstAmount = 0;
                decimal totalSgstAmount = 0;
                decimal totalTax = 0;
                decimal totalDiscount = 0;
                foreach (var iteml in item)
                {

                    totalAmount = totalAmount + iteml.Subtotal;
                    totalTax = iteml.Cgst + iteml.Sgst;
                    decimal baseAmount = iteml.Subtotal * 100 / (100 + totalTax);
                    totalCgstAmount = totalCgstAmount + (iteml.Cgst > 0 ? (baseAmount * iteml.Cgst) / 100 : 0);
                    totalSgstAmount = totalSgstAmount + (iteml.Cgst > 0 ? (baseAmount * iteml.Sgst) / 100 : 0);
                    totalDiscount = totalDiscount + iteml.DiscountedAmount;

                    billHtml += " <tr style ='background-color #f9f9f9; display table-row; vertical-align inherit; " +

"border-color inherit; '>" +
                                           "<td style = 'margin:0; padding:0;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + count.ToString() + "</td>" +
                                                 "<td style = 'margin:0; padding:0;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + iteml.ProductName + "<br/>HSN Code : " +

iteml.HsnCode.ToString() + "</td> " +

                                        "<td style = 'margin:0; padding:0; text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + iteml.Quantity.ToString() + "</td>" +
                                                 "<td style = 'margin:0; padding:0;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" + iteml.OurPrice.ToString() + "</td>" +
                                                 "<td style = 'margin:0; padding:0;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>Rs. " + iteml.DiscAmt.ToString() + "</td>" +
                                                " <td style = 'margin:0; padding:0;  text-align:center; border:1px solid #555; line-" +

"height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>Rs. " + iteml.Subtotal.ToString() + "</td>" +

                                                 "<td style = 'margin:0; padding:0; height:100%; border:1px solid #555; line-height: " +

"1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" +
                                                     "<table width = '100%' height = '100%' cellspacing = '0' cellpadding = '0' style = '" +

"margin: 0; padding: 0; border-collapse:collapse; font-size:12px; '>" +
                                                     "<tr> " +

                                                         "<td style = 'border:none!important;  width:50%!important;  border-" +

"collapse: collapse; text-align:center; margin: 0; padding: 0; border-right:1px solid #555!important;'>" + iteml.Cgst.ToString() + "</td>" +
                                                    "<td style = 'border:none!important; width:50%!important; border-" +

"collapse: collapse; text-align:center; margin: 0; padding: 0; '>" + (iteml.Cgst > 0 ? (baseAmount * iteml.Cgst) / 100 : 0).ToString("0.00") + "</td>" +
                                               " </tr>" +
                                            "</table>" +
                                       " </td>" +

                                        "<td style = 'margin:0; padding:0; height:100%; border:1px solid #555; line-height: " +

"1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>" +
                                                     "<table width = '100%' height = '100%' cellspacing = '0' cellpadding = '0' style ='" +

"margin: 0; padding: 0; border-collapse:collapse; font-size:12px; '>" +
                                                    " <tr>";

                    billHtml += "<td style='border:none!important; width:50%!important; border-" +

        "collapse: collapse; text-align:center; margin: 0; padding: 0; border-right:1px solid #555!important;'>" + iteml.Sgst.ToString() + "</td>" +
                                                            "<td style = 'border:none!important; width:50%!important; border-" +

        "collapse: collapse; text-align:center; margin: 0; padding: 0; '>" + (iteml.Sgst > 0 ? (baseAmount * iteml.Sgst) / 100 : 0).ToString("0.00") + "</td> " +
                                                        "</tr> " +
                                                    "</table> " +
                                                "</td> " +


                                                "<td style = 'margin:0; padding:0;  text-align:center; border:1px solid #555; line-" +

        "height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell; '>Rs. " + iteml.Subtotal.ToString() + "</td>" +
                                                     "</tr>";
                    count = count + 1;

                    //  }
                }
                billHtml += "</tbody>" +
              "</table>" +
          "</div" +

          "<div style = 'width:100%; margin:0 4% 0 0;'>" +
              "<div style='min-height: .01%; overflow-x: auto;'>" +
                "  <table style = 'width: 100%; max-width: 100%; margin-bottom: 20px; background-color: transparent;" +

    "border-spacing: 0; border-collapse: collapse; display: table; font-size:12px;'>" +
    "                           <tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit;'>" +
                         " <tr style='display: table-row; vertical-align: inherit; border-color: inherit;'>" +
                          "    <th style = 'border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:12px; '><b>Total Discount Amount:</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; font-" +

    "size:12px; text-align: right; vertical-align: top;'><b>" + totalDiscount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +


     " <tr style='display: table-row; vertical-align: inherit; border-color: inherit;'>" +
                          "    <th style = 'border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:12px; '><b>Total CGST Tax:</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; font-" +

    "size:12px; text-align: right; vertical-align: top;'><b>" + totalCgstAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +



    "                              <tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +
    "                                 <th style='border-top: 1px solid #f4f4f4; font-weight: bold; text-align: right; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:12px; '><b>Total SGST Tax:</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; font-" +

    "size:12px; text-align: right; vertical-align: top;'><b>" + totalSgstAmount.ToString("0.00") + "</b></td>" +
    "                               </tr>" +
    "                              <tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +
    "                                 <th style='border-top: 1px solid #f4f4f4; font-weight: bold; text-align: left; padding: " +

    "8px; line-height: 1.42857143; vertical-align: top; display: table-cell; font-size:15px;'><b>Total (Payable " +

    "Amount):</b></th>" +
    "                                   <td style = 'border-top: 1px solid #f4f4f4; padding: 8px; line-height: 1.42857143; text-" +

    "align: right; vertical-align: top; font-size:15px;'><b>" + totalAmount.ToString() + "</b></td>" +
    "                               </tr>" +
    "                          </tbody>" +
    "                     </table>" +
    "                </div>" +
    "           </div>" +

    "          <div style = 'width:100%; margin-bottom:10px; margin-top:0; height:1px; background-color:#000;'></div>" +
    "         <div style='width:70%; margin-bottom:20px;  float:left; '>" +
    "            <span>" +
    "               <img src = 'http://behtarzindagi.in/AgriCrm/Content/ImgDealer/pod_tippani.png'/>" +
    "          </span>" +
     "     </div>" +
         " <div style='width:30%; margin-bottom:20px; float:left;'> " +
            "  <p style = 'margin:0; padding:0; text-align:center;'> For Dealer Name</p> " +
             "     <p style = 'margin:0; padding:0; text-align:center; height:50px;'>" +



                 " </p>" +

                  "<p style= 'margin:0; padding:0; text-align:center;'>[Authorised Signatory] </p>" +

              "</div>" +

              "<div style= 'clear:both;'></div>" +


              "<div style= 'width:100%; margin-bottom:10px; margin-top:0; height:1px; background-color:#000;'></div>" +

              "<div style= 'width:70%; margin-bottom:20px;  float:left; '>" +

                "  <span>" +

                 "     <b>" +
                  "        For any query, Please give a missed call on 7876400500." +
                  "</b>" +
              "</span>" +
          "</div>" +
          "<div style = 'width:30%; margin-bottom:20px; float:left;'>" +

                  "<table>" +

                   "   <tr>" +

                    "      <td> Purchase made On </td>" +
                     " <td><img src = 'http://www.behtarzindagi.in/AgriCrm/Content/Img/logo_destp.png'/></td>" +

                     " </tr>" +

                  "</table>" +

             " </div>" +

              "<div style= 'clear:both;'></div>" +

          "</div>" +

         "<div style= 'clear:both'></div " +

      "</div>" +

      "<p style= 'page-break-before: always;'></p>";
            }

            //    billHtml += "</body>" +
            //"</html>";

            return billHtml;
        }

        private static byte[] QRcodeImage(string invoiceNo, string farmerRef, string orderRef)
        {
            // generating a barcode here. Code is taken from QrCode.Net library
            QrEncoder qrEncoder = new QrEncoder(ErrorCorrectionLevel.H);
            QrCode qrCode = new QrCode();
            var barcodeText = "";
            if (invoiceNo == "")
            {
                barcodeText = farmerRef + "|" + orderRef;
            }
            else
            {
                barcodeText = invoiceNo + "|" + farmerRef + "|" + orderRef;
            }
            qrEncoder.TryEncode(barcodeText, out qrCode);
            GraphicsRenderer renderer = new GraphicsRenderer(new FixedModuleSize(4, QuietZoneModules.Four), Brushes.Black, Brushes.White);
            MemoryStream memoryStream = new MemoryStream();
            renderer.WriteToStream(qrCode.Matrix, ImageFormat.Png, memoryStream);
            // very important to reset memory stream to a starting position, otherwise you would get 0 bytes returned
            memoryStream.Position = 0;
            return memoryStream.ToArray();
            //var resultStream = new FileStreamResult(memoryStream, "image/png");
            //resultStream.FileDownloadName = String.Format("{0}.png", barcodeText);
            //return resultStream;

        }
        /* Download PDF
        public static void DownloadFoTripOrdersPDF(List<TripSheetModel> tripOrdersList, string sendToList, string sendCCList, string mailSubject, string mailBody, string fName, int userId)
        {
            // create the HTML to PDF converter
            HtmlToPdf htmlToPdfConverter = new HtmlToPdf();

            // set browser width
            htmlToPdfConverter.BrowserWidth = int.Parse("1200");

            // set browser height if specified, otherwise use the default

            htmlToPdfConverter.BrowserHeight = int.Parse("900");

            // set HTML Load timeout
            htmlToPdfConverter.HtmlLoadedTimeout = int.Parse("120");

            // set PDF page size and orientation
            htmlToPdfConverter.Document.PageSize = GetPageSize();
            htmlToPdfConverter.Document.PageOrientation = GetSelectedPageOrientation();

            // set PDF page margins
            htmlToPdfConverter.Document.Margins = new PdfMargins(0);

            // set a wait time before starting the conversion
            htmlToPdfConverter.WaitBeforeConvert = int.Parse("2");

            // convert HTML to PDF
            byte[] pdfBuffer = null;

            //if (radioButtonConvertUrl.Checked)
            //{
            //    // convert URL to a PDF memory buffer
            //    string url = textBoxUrl.Text;

            //    pdfBuffer = htmlToPdfConverter.ConvertUrlToMemory(url);
            //}
            //else
            //{
            //    // convert HTML code
            //    string htmlCode = textBoxHtmlCode.Text;
            //    string baseUrl = textBoxBaseUrl.Text;

            // convert HTML code to a PDF memory buffer
            //pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(htmlCode, baseUrl);
            pdfBuffer = htmlToPdfConverter.ConvertHtmlToMemory(GetTripSheetHTML(tripOrdersList), "www.goggle.co.in");

            // }

            // inform the browser about the binary data format
            HttpContext.Current.Response.AddHeader("Content-Type", "application/pdf");

            // let the browser know how to open the PDF document, attachment or inline, and the file name
            bool attachment = true;
            HttpContext.Current.Response.AddHeader("Content-Disposition", String.Format("{0}; filename=HtmlToPdf.pdf; size={1}",
                attachment ? "inline" : "attachment", pdfBuffer.Length.ToString()));

            // write the PDF buffer to HTTP response
            HttpContext.Current.Response.BinaryWrite(pdfBuffer);

            // call End() method of HTTP response to stop ASP.NET page processing
            HttpContext.Current.Response.End();
        }
        */
        #endregion

        #region Fo Not using App last 1 hour
        public static void SendMailWithFoNotUpdateAppData(DataTable dataTable, string sendToList, string sendCCList, string mailSubject, string mailBody, string fileName, int userId)
        {
            try
            {
                //DataTable dataTable = gettab();
                DataSet ds = new DataSet();

                var dt = dataTable.Copy();
                dt.TableName = "Table1";
                // ds.Tables.Add(dt);
                mailBody = GetHtmlForFONotRespondLastHour(dt);

                var mailFrom = Convert.ToString(ConfigurationManager.AppSettings["UMail"]);
                var password = Convert.ToString(ConfigurationManager.AppSettings["UMailPass"]);
                MailMessage mail = new MailMessage();

                mail.From = new MailAddress(Convert.ToString(ConfigurationManager.AppSettings["UMailFrom"]));

                var sendArrToList = sendToList.Split(',');
                var sendArrCCList = sendCCList.Split(',');

                //Start Test
                //mail.To.Add("arpit.jain@handygo.com");
                foreach (var toName in sendArrToList)
                {
                    if (toName != "")
                        mail.To.Add(toName);
                }
                //End
                foreach (var ccName in sendArrCCList)
                {
                    if (ccName != "")
                        mail.CC.Add(ccName);
                }

                mail.Subject = mailSubject;//sheetType.Subject + " " + fromOrderDate;

                mail.Body = mailBody;// string.Format(mailBody, "ext"); //string.Format(sheetType.Body, fromOrderDate);
                mail.IsBodyHtml = true;
                
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls;
                SmtpClient smtp = new SmtpClient
                {
                    Host = Convert.ToString(ConfigurationManager.AppSettings["UMailHost"]), // smtp server address here…
                    Port = Convert.ToInt32(ConfigurationManager.AppSettings["UMailPort"]),
                    EnableSsl = true,
                    UseDefaultCredentials = false,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    Credentials = new NetworkCredential(mailFrom, password),
                    Timeout = 30000,
                };
                smtp.TargetName = "STARTTLS/mail.handygo.com";
                ServicePointManager.ServerCertificateValidationCallback = delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
                smtp.Send(mail);
                LogDal.MailLog(sendToList, sendCCList, mailSubject, mailBody, userId);
            }
            catch (Exception ex)
            {
                LogDal.ErrorLog("Helper/ReportInXMail", MethodBase.GetCurrentMethod().Name, ex.Message, userId);

            }

        }

        private static string  GetHtmlForFONotRespondLastHour(DataTable DT)
        {
            string html = "";
            html += "<!DOCTYPE html>" +
         "<html>" +
         "<head>" +

             "<title></title>" +

             "<meta charset = 'utf-8'/>" +
          "</head>" +
          "<body>" +


      "<body>" +
    "<div style = 'font-family: 'Source Sans Pro',sans-serif!important; background-color:#fff; font-size: 13px!important;   line-height: 1.42857143;     -webkit-font-smoothing: antialiased; color: #333;'>" +

           "<div style = ' position relative; background #fff; border 1px solid #f4f4f4; max-width:1000px; margin:0 auto;'>" +


                "<div style = 'clear:both;'></div>" +


                 "<div style = 'overflow-x: auto;'>" +

                      "<table cellspacing = '0' cellpadding = '0' style = 'width: 100%; border:1px solid #ccc; max-width: 100%; background-color: transparent; border-spacing: 0; border-collapse: collapse; display: table;'>" +

                               "<thead style = 'display: table-header-group; vertical-align: middle; border-color: inherit;'>" +

                                    "<tr style = 'display: table-row; vertical-align: inherit; border-color: inherit;'>" +

                                    "<th style='text - align:center; border: 1px solid #ccc; margin:0; padding:5px;'>S No.</th>"+

                                         "<th style = 'text-align:center; border:1px solid #ccc; margin:0; padding:5px;'> FO Name </th>" +

                                              "<th style = 'text-align:center; border:1px solid #ccc; margin:0; padding:5px;'> Mobile </th>" +

                                               "<th style = 'text-align:center; border:1px solid #ccc; margin:0; padding:5px;'> District </th>" +

                                                "<th style = 'text-align:center; border:1px solid #ccc; margin:0; padding:5px;'> Last Update </th>" +

                                                 "</tr>" +

                                             "</thead>" +


                                             "<tbody style = 'display: table-row-group; vertical-align: middle; border-color: inherit; text-align:left; border-collapse:collapse;'>";
                                             for (int i = 0; i < DT.Rows.Count; i++)
                                                {
                	
                html += "<tr style = ' background-color #f9f9f9; display table-row; vertical-align inherit; border-color inherit;'>" +

                    "<td style='margin: 0; padding: 5px; text-align:center; border: 1px solid #ccc; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>"+(i+1)+"</td>"+

                     "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #ccc; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'> " +DT.Rows[i]["FO Name"].ToString()+" </td>" +

                      "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #ccc; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>  " + DT.Rows[i]["Mobile"].ToString() + "  </td>" +

                       "<td style = 'margin:0; padding:5px;  text-align:center; border:1px solid #ccc; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>  " + DT.Rows[i]["District"].ToString() + "  </td>" + //   

                        "<td style = 'margin:0; padding:5px; text-align:center; border:1px solid #ccc; line-height: 1.42857143; vertical-align: top; box-sizing: border-box; display: table-cell;'>  " + DT.Rows[i]["Last Update"].ToString() + "  </td>" +

                     "</tr>";
                                                 }

                                    html += "</tbody>" +

                                               "</table>" +

                                           "</div>" +

                                       "</div>" +

                                       "<div style = 'clear:both'></div>" +

                                    "</div>" +
                                "</body>" +
                                "</html>";

            return html;
        }
        #endregion

    }
}
