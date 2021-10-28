using CefSharp;
using CefSharp.WinForms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NiboriumBrowser
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            CefSettings settings = new CefSettings();
            Cef.Initialize(settings);
            txtUrl.Text = "https://duckduckgo.com/";
            ChromiumWebBrowser chrome = new ChromiumWebBrowser(txtUrl.Text);
            chrome.Parent = tabControl.SelectedTab;
            chrome.Dock = DockStyle.Fill;
            chrome.AddressChanged += Chrome_AdressChanged;
            chrome.TitleChanged += Chrome_TitleChanged;
        }

        private void Chrome_AdressChanged(object seneder, AddressChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                txtUrl.Text = e.Address;
            }));
        }

        private void btnGo_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (chrome != null)
            {
                chrome.Load(txtUrl.Text);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (chrome != null)
            {
                chrome.Refresh();
            }
        }

        private void btnForward_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (chrome != null)
            {
                if (chrome.CanGoForward)
                    chrome.Forward();
            }
        }

        private void btnBack_Click(object sender, EventArgs e)
        {
            ChromiumWebBrowser chrome = tabControl.SelectedTab.Controls[0] as ChromiumWebBrowser;
            if (chrome != null)
            {
                if (chrome.CanGoBack)
                    chrome.Back();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            Cef.Shutdown();
        }

        private void btnNewTab_Click(object sender, EventArgs e)
        {
            TabPage tab = new TabPage();
            tab.Text = "New tab";
            tabControl.Controls.Add(tab);
            tabControl.SelectTab(tabControl.TabCount - 1);
            ChromiumWebBrowser chrome = new ChromiumWebBrowser("https://duckduckgo.com/");
            chrome.Parent = tab;
            chrome.Dock = DockStyle.Fill;
            txtUrl.Text = "https://duckduckgo.com/";
            chrome.AddressChanged += Chrome_AdressChanged;
            chrome.TitleChanged += Chrome_TitleChanged;
        }

        private void Chrome_TitleChanged(object sender, TitleChangedEventArgs e)
        {
            this.Invoke(new MethodInvoker(() =>
            {
                tabControl.SelectedTab.Text = e.Title;
            }));
        }
    }
}