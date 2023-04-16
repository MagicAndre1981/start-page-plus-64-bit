﻿using System;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;

using Luminous.Code.Extensions.Exceptions;
using Luminous.Code.Extensions.Strings;

namespace StartPagePlus.UI.Views.RecentItems
{
    using ViewModels;
    using ViewModels.RecentItems;

    public partial class RecentItemsView : UserControl
    {
        public RecentItemsView() // constructor injection doesn't seem to work for views
        {
            InitializeComponent();

            try
            {
                var viewModel = ViewModelManager.RecentItemsViewModel;

                // NOTE: Refresh is called in viewmodel's constructor


                var view = (ListCollectionView)CollectionViewSource.GetDefaultView(viewModel.Items);

                using (view.DeferRefresh())
                {
                    AddGrouping(view);
                    AddSorting(view);
                    AddFilter(view);
                }

                OnFilterTextChangedRefreshView(view);
                OnloadedSetSelectedItemToNull();

                //RootMethods.ListenFor<RecentItemsRefresh>(this, FocusFilterTextBox);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ExtendedMessage()); //YD: replace all MessageBox.Show calls with DialogService calls
            }
        }

        private static void AddGrouping(ListCollectionView view)
        {
            view.GroupDescriptions.Clear();
            view.IsLiveGrouping = true;
            view.GroupDescriptions.Add(new PropertyGroupDescription(nameof(RecentItemViewModel.PeriodType)));
        }

        private static void AddSorting(ListCollectionView view)
        {
            view.SortDescriptions.Clear();
            view.IsLiveSorting = true;
            view.SortDescriptions.Add(new SortDescription(nameof(RecentItemViewModel.PeriodType), ListSortDirection.Ascending));
            view.SortDescriptions.Add(new SortDescription(nameof(RecentItemViewModel.Date), ListSortDirection.Descending));
        }

        // https://joshsmithonwpf.wordpress.com/2007/06/12/searching-for-items-in-a-listbox/

        private void AddFilter(ListCollectionView view)
        {
            view.Filter = (object obj) =>
            {
                if (string.IsNullOrEmpty(FilterTextBox.Text))
                    return true;

                if (!(obj is RecentItemViewModel item))
                    return false;

                var name = item.Name;

                return name.IsNotNullOrEmpty()
                    && name.MatchesFilter(FilterTextBox.Text);
            };
        }

        private void OnFilterTextChangedRefreshView(ListCollectionView view)
            => FilterTextBox.TextChanged += (object sender, TextChangedEventArgs e)
                => view.Refresh();

        private void OnloadedSetSelectedItemToNull()
            => RecentItemsListView.Loaded += (sender, e)
                => RecentItemsListView.SelectedItem = null;

        private void ClearFilterText_Click(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Text = "";
            FocusFilterTextBox();
        }

        private void FocusFilterTextBox(object sender = null)
            => FilterTextBox.Focus();

        private void OnExpanded(object sender, RoutedEventArgs e)
            => FocusFilterTextBox();

        private void OnCollapsed(object sender, RoutedEventArgs e)
            => FocusFilterTextBox();

        private void OnSelectionChanged(object sender, SelectionChangedEventArgs e)
            => FocusFilterTextBox();
    }
}