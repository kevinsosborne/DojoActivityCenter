using System.ComponentModel.DataAnnotations;
using System;
using System.Collections.Generic;

namespace DojoActivityCenter.Models

{
    public class Participant : BaseEntity
    {
        public int ParticipantId {get; set;}
        public int ActivityId {get;set;}
        public Activity Activity {get; set;}
        public int UserId {get;set;}
        public User User {get; set;}

    }
}