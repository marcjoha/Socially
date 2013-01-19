using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using ScApi;
using ScApi.Data;
using Socially.ViewModels;

namespace Socially.Views
{
    /// <summary>
    ///     Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly SocialcastApi _scapi;
        private readonly Community _community;

        private CancellationTokenSource _cancelDaemon = new CancellationTokenSource();
        private readonly Dictionary<string, int> _streams = new Dictionary<string, int>();
        
        private MainViewModel _mainViewModel;

        public MainWindow()
        {
            InitializeComponent();

            _scapi = new SocialcastApi("https://demo.socialcast.com", "emily@socialcast.com", "demo");

            // Get community info
            _community = _scapi.GetCommunity();

            // Find all stream ids, so we can fetch messages later on
            var streams = _scapi.GetStreams();
            _streams.Add("Home Stream", streams.Where(x => x.Name.Equals("Home Stream")).Select(x => x.Id).First());
            _streams.Add("@Mentions", streams.Where(x => x.Name.Equals("@Mentions")).Select(x => x.Id).First());
            _streams.Add("Sent", streams.Where(x => x.Name.Equals("Sent")).Select(x => x.Id).First());
            _streams.Add("Company Stream", streams.Where(x => x.Name.Equals("Company Stream")).Select(x => x.Id).First());

            // Don't hook up the model before the window is loaded
            Loaded += MainWindowLoaded;
        }

        private void MainWindowLoaded(object sender, RoutedEventArgs e)
        {
            _mainViewModel = new MainViewModel(_community);
            DataContext = _mainViewModel;
        }

        private void StreamTabs_OnSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Cancel any currently running daemon from other tab
            _cancelDaemon.Cancel();
            _cancelDaemon = new CancellationTokenSource();

            if (HomeStreamTab.IsSelected)
            {
                Daemon(UpdateHomeStream);
                return;
            }
            if (MentionsTab.IsSelected)
            {
                Daemon(UpdateMentionsStream);
                return;
            }
            if (SentTab.IsSelected)
            {
                Daemon(UpdateSentStream);
                return;
            }
            if (CompanyStreamTab.IsSelected)
            {
                Daemon(UpdateCompanyStream);
                return;
            }
        }

        private void Daemon(Action action)
        {
            CancellationToken ct = _cancelDaemon.Token;
            Task.Factory.StartNew(() =>
                                      {
                                          while (true)
                                          {
                                              action();

                                              Thread.Sleep(100);

                                              // If a user has switched tabs, a cancellation token is issued
                                              if (ct.IsCancellationRequested)
                                              {
                                                  break;
                                              }
                                          }
                                      }, ct);
        }

        private void UpdateHomeStream()
        {
            var res = new List<MessageViewModel>(GetStream(_streams["Home Stream"]));
            Dispatcher.Invoke(() => _mainViewModel.HomeStream = new ObservableCollection<MessageViewModel>(res));
        }

        private void UpdateMentionsStream()
        {
            var res = new List<MessageViewModel>(GetStream(_streams["@Mentions"]));
            Dispatcher.Invoke(() => _mainViewModel.MentionsStream = new ObservableCollection<MessageViewModel>(res));
        }

        private void UpdateSentStream()
        {
            var res = new List<MessageViewModel>(GetStream(_streams["Sent"]));
            Dispatcher.Invoke(() => _mainViewModel.SentStream = new ObservableCollection<MessageViewModel>(res));
        }

        private void UpdateCompanyStream()
        {
            var res = new List<MessageViewModel>(GetStream(_streams["Company Stream"]));
            Dispatcher.Invoke(() => _mainViewModel.CompanyStream = new ObservableCollection<MessageViewModel>(res));
        }

        private IEnumerable<MessageViewModel> GetStream(int id)
        {
            return _scapi.GetStream(id).Select(message => new MessageViewModel(message)).ToList();
        }
    }
}