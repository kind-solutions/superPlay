//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>

namespace Superplay.Models
{
    public class Player
    {
        public Guid Id { get; set; }
        public ICollection<Device> Devices { get; } = new List<Device>();

        public int Coins {get; set;}
        public int Rolls {get; set;}
    }
}