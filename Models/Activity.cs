using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DojoActivityCenter.Models
{
    public class Activity : BaseEntity
    {
        [Key]
        public int ActivityId {get;set;}
        public string Title {get;set;}
        public DateTime Time {get;set;}
        public DateTime Date {get;set;}
        public int Duration {get; set;}
        public string DurationType {get; set;}
        public string Description {get; set;}
        public DateTime CreatedAt {get; set;}
        public DateTime UpdatedAt {get; set;}
        public int UserId {get; set;}
        public User User {get; set;}
        public List<Participant> Participants {get; set;}
        public Activity()
        {
            Participants = new List<Participant>();
        }
    }
}