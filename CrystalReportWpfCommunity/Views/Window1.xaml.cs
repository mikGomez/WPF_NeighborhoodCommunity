﻿using CrystalDecisions.CrystalReports.Engine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CrystalReportWpfCommunity
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        public Window1()
        {
            InitializeComponent();
            reportViewer.Owner = this;
            ReportDocument report = new ReportDocument();
            string path = "..\\..\\Report\\CrystalReport1.rpt";
            report.Load(path);
            //Lo dejo comentado ya que sino pide ID y contraseña

            //report.Refresh();
            reportViewer.ViewerCore.ReportSource = report;
        }
    }
}
