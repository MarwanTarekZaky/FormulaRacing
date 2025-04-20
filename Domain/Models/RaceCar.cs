namespace Domain.Models
{
    public class RaceCar
    {
        public int RaceId { get; set; }
        public Race Race { get; set; } = null!;

        public int CarId { get; set; }
        public Car Car { get; set; } = null!;
    }
}