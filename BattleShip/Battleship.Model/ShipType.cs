namespace Battleship.Model
{
    /* The aircraft carrier piece takes up five grid spaces,
     * the battleship utilizes four, the destroyer and submarine
     * both take up three spaces and the patrol boat is the
     * smallest with two grid spots */
    public enum ShipType
    {
        AircraftCarrier,
        Battleship,
        Destroyer,
        Submarine,
        PatrolBoat
    }
}
