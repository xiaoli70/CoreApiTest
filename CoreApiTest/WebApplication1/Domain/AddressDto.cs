namespace Net6Api.Domain
{
    public class AddressDto
    {
        public int X { get; }
        public int Y { get; }

        public AddressDto(int x, int y) => (X, Y) = (x++, ++y);
    }
}