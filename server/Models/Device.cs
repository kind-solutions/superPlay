//Copyright 2023 Niculae Ioan-Paul <niculae.paul@gmail.com>


namespace Superplay.Models
{
    public enum DeviceType
    {
        Android,
        iOS,
    }
    public class Device
    {
        public Guid Id { get; set; }

        public DeviceType? Type { get; set; }

        public Guid PlayerId { get; set; }
        public Player Player {get; set;} = null!; 

    }
}