using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SavePacket.Models
{
    public class Packet
    {
        public string SourceIP { get; set; }
        public DateTime Time { get; set; }
        public long Length { get; set; }
    }
}
