using FsCheck;
using FsCheck.Xunit;

namespace WithPropertyBasedTesting
{
    public class AddTests
    {
        [Property]
        public Property Multiply_With_2_Is_The_Same_As_Adding_The_Same_Number(int x)
        {
            return (x * 2 == Add(x, x)).Collect("Values together: " + (x * 2));
        }

        [Property]
        public Property Adding_1_Twice_Is_The_Same_As_Adding_Two(int x)
        {
            return (Add(1, Add(1, x)) == Add(x, 2)).ToProperty();
        }

        [Property]
        public Property Adding_Two_Numbers_Doesnt_Depend_On_Parameter_Order(int x, int y)
        {
            return (Add(x, y) == Add(y, x)).ToProperty();
        }

        private int Add(int x, int y)
        {
            return x + y;
        }
    }
}
