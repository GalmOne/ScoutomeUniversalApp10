using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Scoutome.Model
{
    [DataContract]
    public class Reunion
    {
        [DataMember]
        public List<Anime> animes { get; set; }
        [DataMember]
        public double codeReunion { get; set; }
        [DataMember]
        public string libelle { get; set; }
        [DataMember]
        public string dateReunion { get; set; }
        [DataMember]
        public string lieu { get; set; }
        [DataMember]
        public double prix { get; set; }
    }
}
