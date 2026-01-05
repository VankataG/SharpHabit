using System.Collections.Specialized;
using System.ComponentModel;
using ScottPlot;
using System.Windows;
using System.Windows.Media;
using SharpHabit.Wpf.ViewModels;

namespace SharpHabit.Wpf
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (_, __) =>
            {
                HookChartToViewModel();
                RenderChart();
            };
        }

        private void HookChartToViewModel()
        {
            if (DataContext is not MainViewModel vm)
                return;

            // Redraw when DayPercents list is rebuilt
            vm.DayPercents.CollectionChanged += (_, __) => RenderChart();

            // If you update Percent inside existing items (optional safety)
            foreach (var item in vm.DayPercents)
                item.PropertyChanged += DayPercent_PropertyChanged;

            // Re-hook when collection changes (adds/removes)
            vm.DayPercents.CollectionChanged += DayPercents_CollectionChanged;
        }

        private void DayPercents_CollectionChanged(object? sender, NotifyCollectionChangedEventArgs e)
        {
            if (DataContext is not MainViewModel vm)
                return;

            if (e.NewItems != null)
                foreach (INotifyPropertyChanged item in e.NewItems)
                    item.PropertyChanged += DayPercent_PropertyChanged;

            if (e.OldItems != null)
                foreach (INotifyPropertyChanged item in e.OldItems)
                    item.PropertyChanged -= DayPercent_PropertyChanged;
        }

        private void DayPercent_PropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == "Percent")
                RenderChart();
        }

        private void RenderChart()
        {
            if (DataContext is not MainViewModel vm)
                return;

            double[] ys = vm.DayPercents.Select(d => d.Percent).ToArray();
            if (ys.Length == 0) return;

            double[] xs = Enumerable.Range(1, ys.Length).Select(i => (double)i).ToArray();

            var plt = CompletionPlot.Plot;
            plt.Clear();

            var green = ScottPlot.Color.FromHex("#2E7D32");

            var sp = plt.Add.Scatter(xs, ys);
            sp.Color = green;
            sp.LineWidth = 2;
            sp.MarkerSize = 0;
            sp.Smooth = true;

            sp.FillY = true;
            sp.FillYValue = 0;
            sp.FillYColor = green.WithAlpha(0.10);

            plt.Axes.SetLimits(1, ys.Length, 0, 100);

            // Blend with UI
            plt.FigureBackground.Color = ScottPlot.Color.FromHex("#F0F4F8");
            plt.DataBackground.Color = ScottPlot.Color.FromHex("#F3F6FB");

            plt.Axes.Frame(false);
            plt.Grid.IsVisible = false;

            // Hide ALL ticks & labels
            plt.Axes.Left.IsVisible = false;
            plt.Axes.Bottom.IsVisible = false;


            CompletionPlot.Refresh();
        }





    }


}