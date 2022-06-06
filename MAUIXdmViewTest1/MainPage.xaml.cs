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
        var xqueryExecutable = xqueryCompiler.Compile("(1 to 5) ! <item>Item {.}</item>");
        var xqueryEvaluator = xqueryExecutable.Load();

        var xdmValue = xqueryEvaluator.Evaluate().ToArray();

        foreach (XdmNode node in xdmValue)
        {
            Debug.WriteLine(node.OuterXml);
        }

        XdmView.ItemsSource = xdmValue;

    }

}

