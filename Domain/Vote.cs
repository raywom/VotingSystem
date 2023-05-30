using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Domain
{
    public class Vote
    {
        public Guid Id { get; set; }
        public string AppUserId { get; set; }
        public AppUser AppUser { get; set; }
        public Guid PollId { get; set; }
        public Poll Poll { get; set; }
        public bool IsHost { get; set; }
        public Guid ChoiceId { get; set; }
        public Choice Choice { get; set; }
    }
}