using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entity
{
    public class Ozontel_CallLog_Model
    {
        public string AgentPhoneNumber { set; get; }
        public string Disposition { set; get; }
        public string CallerConfAudioFile { set; get; }
        public string TransferredTo { set; get; }
        public string Apikey { set; get; }
        public string Did { set; get; }
        public string StartTime { set; get; }
        public string CallDuration { set; get; }
        public string EndTime { set; get; }
        public string ConfDuration { set; get; }
        public string CustomerStatus { set; get; }
        public string TimeToAnswer { set; get; }
        public string monitorUCID { set; get; }
        public string AgentID { set; get; }
        public string AgentStatus { set; get; }
        public string Location { set; get; }
        public string FallBackRule { set; get; }
        public string CampaignStatus { set; get; }
        public string CallerID { set; get; }
        public string Duration { set; get; }
        public string Status { set; get; }
        public string AgentUniqueID { set; get; }
        public string UserName { set; get; }
        public string HangupBy { set; get; }
        public string AudioFile { set; get; }
        public string PhoneName { set; get; }
        public string TransferType { set; get; }
        public string DialStatus { set; get; }
        public string CampaignName { set; get; }
        public string UUI { set; get; }
        public string AgentName { set; get; }
        public string Skill { set; get; }
        public string DialedNumber { set; get; }
        public string Type { set; get; }
        public string Comments { set; get; }
    }

    public class Ozontel_CallLog_Model2
    {
        public long Id { get; set; }
        public string AgentPhoneNumber { set; get; }
        public string Disposition { set; get; }
        public string CallerConfAudioFile { set; get; }
        public string TransferredTo { set; get; }
        public string Apikey { set; get; }
        public string Did { set; get; }
        public string StartTime { set; get; }
        public string CallDuration { set; get; }
        public string EndTime { set; get; }
        public string ConfDuration { set; get; }
        public string CustomerStatus { set; get; }
        public string TimeToAnswer { set; get; }
        public string monitorUCID { set; get; }
        public string AgentID { set; get; }
        public string AgentStatus { set; get; }
        public string Location { set; get; }
        public string FallBackRule { set; get; }
        public string CampaignStatus { set; get; }
        public string CallerID { set; get; }
        public string Duration { set; get; }
        public string Status { set; get; }
        public string AgentUniqueID { set; get; }
        public string UserName { set; get; }
        public string HangupBy { set; get; }
        public string AudioFile { set; get; }
        public string PhoneName { set; get; }
        public string TransferType { set; get; }
        public string DialStatus { set; get; }
        public string CampaignName { set; get; }
        public string UUI { set; get; }
        public string AgentName { set; get; }
        public string Skill { set; get; }
        public string DialedNumber { set; get; }
        public string Type { set; get; }
        public string Comments { set; get; }
        public string CreateDate { get; set; }
    }

    public class CallLog_Model
    {
        public string EventName { set; get; }
        public string ANI { set; get; }
        public string DNIS { set; get; }
        public string Mode { set; get; }
        public string CallId { set; get; }
        public string UserLogin { set; get; }
        public string Campaign { set; get; }
        public string LeadId { set; get; }
        public string Skill { set; get; }
        public string dnisIB { set; get; }
    
        
    }

}
