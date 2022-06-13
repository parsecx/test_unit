using mycaller.preproc;
using mycaller.models;
using System.Text;

//Entering date for queries
Console.WriteLine("Please enter the date_from in format YYYY-MM-DD");
string? date_from = Console.ReadLine();
Console.WriteLine("Please enter the date_to in format YYYY-MM-DD");
string? date_to = Console.ReadLine();
Console.WriteLine("Getting your data...");

//Getting values from API's 
IPO t = await PreProc.GetConnection_IPO(date_from, date_to);
MA m = await PreProc.GetConnection_MA(date_from, date_to);
Earnings r = await PreProc.GetConncetion_Earnings(date_from, date_to);
Dividents d = await PreProc.GetConnection_Div(date_from, date_to);

//Getting max dividents 
double max_div = 0;
foreach (Dividend? item in d.dividends)
{
    char[]? dividents = item.dividend.ToArray();
    dividents[1] = ',';
    string s = new string(dividents);
    if (max_div < Convert.ToDouble(s))
        max_div = Convert.ToDouble(s);
}

//Working with txt file
if (!File.Exists("benzinga.txt"))
    try
    {
        using(StreamWriter s = new StreamWriter("benzinga.txt",false,Encoding.Default))
        {
            s.WriteLine("DateFrom DateTo M&A IPOs Earnings Dividents");
            s.WriteLine(date_from + " " + date_to + " " + m.ma.Length + " " + t.ipos.Length + " " + r.earnings.Length + " " + max_div.ToString());
            Console.WriteLine("File was created! You can check it at local directory");
        }
    }
    catch(Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
else
    try 
    { 
        using (StreamWriter s = new StreamWriter("benzinga.txt", true, Encoding.Default))
        {
            s.WriteLine(date_from + " " + date_to + " " + m.ma.Length + " " + t.ipos.Length + " " + r.earnings.Length + " " + max_div.ToString());
            Console.WriteLine("New line was added! You can check it at local directory");
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.Message);
    }
Console.ReadKey();

