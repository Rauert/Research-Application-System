using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.HtmlControls;
using System.Web.UI;

/**
 * Factory to create html cell objects.
 */
public class HTMLFactory
{
	public HTMLFactory(){}

    public static HtmlTableCell buildCell(string width, string align, string val)
    {
        HtmlTableCell cell = new HtmlTableCell();
        cell.Width = width;
        cell.Align = align;
        cell.InnerText = val;
        return cell;
    }

    public static HtmlTableCell buildCell(string width, string align, bool val, HtmlInputCheckBox chk)
    {
        chk.Checked = val;
        HtmlTableCell cell = new HtmlTableCell();
        cell.Width = width;
        cell.Align = align;
        cell.Controls.Add(chk);
        return cell;
    }

    public static HtmlTableCell buildCell(string width, string align, Control cntrl)
    {
        HtmlTableCell cell = new HtmlTableCell();
        cell.Width = width;
        cell.Align = align;
        cell.Controls.Add(cntrl);
        return cell;
    }
}