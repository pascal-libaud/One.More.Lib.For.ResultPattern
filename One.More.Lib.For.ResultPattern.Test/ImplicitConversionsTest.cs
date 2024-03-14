namespace One.More.Lib.For.ResultPattern.Test;

public class ImplicitConversionsTest
{
    [Fact]
    public void Implicit_conversion_from_T_to_result_of_T_should_work()
    {
        var result = GetSuccess();
        result.IsSuccess.Should().BeTrue();
        result.Value.Should().Be(1);
        result.Errors.Should().BeEmpty();
    }

    [Fact]
    public void Implicit_conversion_from_result_of_T_to_T_should_work()
    {
        var result = GetSuccess();
        Test(result).Should().Be(1);
    }

    [Fact]
    public void Implicit_conversion_should_not_work_with_fluent_assertions()
    {
        var result = GetSuccess();
        result.Should().NotBe(1);
    }

    private static Result<int> GetSuccess()
    {
        return 1;
    }

    private static int Test(int value)
    {
        return value;
    }
}