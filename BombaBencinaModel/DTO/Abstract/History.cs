using System;

namespace BombaBencinaModel.DTO.Abstract
{
    public abstract class History
    {
        private int stationId;
        private DateTime dateSent;
        private string comment;

        public int StationId { get => stationId; set => stationId = value; }
        public DateTime DateSent { get => dateSent; set => dateSent = value; }
        public string Comment { get => comment; set => comment = value; }
    }
}
