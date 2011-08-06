namespace Moq
{
    public class FakeClass
    {
        public IFakeService FakeService { get; set; }

        public int Calculate()
        {
            return FakeService.DoWork("test");
        }
    }
}
