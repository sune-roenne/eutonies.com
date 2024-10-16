using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Html;
using System.Globalization;

namespace Eutonies.UI.Pages.Home;

public partial class HomeWordComponent
{
    private static CultureInfo EnUs = CultureInfo.GetCultureInfo("en-US");
    private static readonly Random Random = new Random();
    private static long _currentId = 0L;
    private static object _lock = new { };
    private static long NextId()
    {
        lock(_lock )
            return _currentId++;
    } 


    [Parameter]
    public string Word { get; set; } = "Word";

    [Parameter]
    public int InitialDelaySeconds { get; set; }

    [Parameter]
    public int SecondsToAnimate { get; set; }


    private readonly int _zIndex = Random.Next(100, 200);
    private readonly int _fontSize = Random.Next(10, 20);
    private readonly long _id = NextId();
    private readonly int ColorRed = 255 - Random.Next(55);
    private readonly int ColorGreen = 255 - Random.Next(55);
    private readonly int ColorBlue = 255 - Random.Next(55);


    private readonly double _startX;
    private readonly double _startY;
    private readonly double _endX;
    private readonly double _endY;
    private readonly double _middleX;
    private readonly double _middleY;


    private string StX => Format(_startX * 100d);
    private string StY => Format(_startY * 100d);
    private string MiX => Format(_middleX * 100d);
    private string MiY => Format(_middleY * 100d);
    private string EndX => Format(_endX * 100d);
    private string EndY => Format(_endY * 100d);



    private static string Format(double d) => double.Round(d, 4).ToString("F4", EnUs); 



    private string WordId => $"eutonies-home-word-{_id}";

    public HomeWordComponent()
    {
        var (lowerX, upperX) = (Random.NextDouble() * 0.5, 1.0 - Random.NextDouble() * 0.5);
        var (lowerY, upperY) = (Random.NextDouble() * 0.5, 1.0 - Random.NextDouble() * 0.5);
        if(Random.Next(2) == 1)
            (_startX, _endX) = (lowerX, upperX);
        else
            (_endX, _startX) = (lowerX, upperX);

        if (Random.Next(2) == 1)
            (_startY, _endY) = (lowerY, upperY);
        else
            (_endY, _startY) = (lowerY, upperY);
        _middleX = lowerX + Random.NextDouble() * 0.5 * (upperX - lowerX);
        _middleY = lowerY + Random.NextDouble() * 0.5 * (upperY - lowerY);
    }


    private HtmlString CssWordRule => new HtmlString(
$@".{WordId} {{
   color: rgb({ColorRed},{ColorGreen}, {ColorBlue});
   opacity: 0;
   z-index: {_zIndex};
   font-size: {_fontSize}pt;
   animation: {WordId}-animate {SecondsToAnimate}s ease-in-out {InitialDelaySeconds}s 1;
}}");
    private HtmlString CssAnimateWordRule => new HtmlString(
$@"@keyframes {WordId}-animate {{
   0% {{
      opacity: 0;
      top: {StY}vh;
      left: {StX}vw;
   }}
   10% {{
      opacity: 0.9;
   }}
   
   25% {{
      opacity: 1;
      scale: 150%;
   }}
   50% {{
      top: {MiY}vh;
      left: {MiX}vw;
      scale: 200%;
   }}
   75% {{
      scale: 150%;
      opacity: 0.8;
   }}

   100% {{
      top: {EndY}vh;
      left: {EndX}vw;
      scale: 1%;
      opacity: 0;
   }}
}}");


}
