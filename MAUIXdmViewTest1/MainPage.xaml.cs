namespace MAUIXdmViewTest1;
using Saxon.Api;
using System.Diagnostics;

public partial class MainPage : ContentPage
{
	private static Processor processor = new Processor();

	public MainPage()
	{
		InitializeComponent();

        //var values = new string[] { "foo", "bar", "baz" };

        //var values = new XdmItem[] { new XdmAtomicValue("foo"), new XdmAtomicValue(3), new XdmAtomicValue(true) };

        var xqueryCompiler = processor.NewXQueryCompiler();
        xqueryCompiler.BaseUri = new Uri("urn:from-string");
        var xqueryExecutable = xqueryCompiler.Compile("(1 to 3) ! <item>Item {.}</item>, random-number-generator(current-dateTime())?permute(1 to 20)[position() le 3], array { 1 to 3}, random-number-generator(current-dateTime())");
        var xqueryEvaluator = xqueryExecutable.Load();

        var xdmValue = xqueryEvaluator.Evaluate();

        var selectionList = xdmValue.Select(item => new XdmItemSelection(item)).ToList();

        foreach (var item in selectionList)
        {
            Debug.WriteLine(item.Serialization);
        }

        XdmView.ItemsSource = selectionList;

    }

}

public record XdmItemSelection(XdmItem Item)
{
    public string Serialization { get; init; } = Item.ToString();
}

