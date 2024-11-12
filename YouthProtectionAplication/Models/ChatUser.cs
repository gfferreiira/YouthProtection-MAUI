using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace YouthProtectionAplication.Models
{
    public class ChatUser
    {
        public long Id { get; set; }
        public long ChatId { get; set; }

        public long SenderId  { get; set; }

        public string Content { get; set; }

        public DateTime SentAt { get; set; }

        public string DataConvertida => SentAt.ToString("HH:mm dd/MM");


        public bool IsMessageFromCurrentUser { get; set; }
    }
}
