using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Windows.Controls;
using Prism.Regions;

namespace CommonLibUi.RegionAdapters
{
	public class TabControlRegionAdapter : RegionAdapterBase<TabControl>
	{
		private TabControl _regionTarget;

		public TabControlRegionAdapter(IRegionBehaviorFactory factory)
			: base(factory)
		{
		}

		protected override void Adapt(IRegion region, TabControl regionTarget)
		{
			_regionTarget = regionTarget;
			region.Views.CollectionChanged -= ItemsCollectionChanged;
			region.Views.CollectionChanged += ItemsCollectionChanged;
		}

		private void ItemsCollectionChanged(object s, NotifyCollectionChangedEventArgs e)
		{
			if (e.Action != NotifyCollectionChangedAction.Add)
				return;
			var items = _regionTarget.Items;

			var anyTabActive = false;
			foreach (ITabItemView tab in e.NewItems.Cast<ITabItemView>().OrderBy(it => it.Index))
			{
				if (tab.Index > items.Count)
					items.Add(tab);
				else
					items.Insert(tab.Index, tab);

				if (tab.IsActive)
				{
					anyTabActive = true;
					_regionTarget.SelectedItem = tab;
					foreach (var tabItemView in items.SourceCollection.Cast<ITabItemView>()
						.Where(it => it != tab))
					{
						tabItemView.IsActive = false;
					}
				}
				if (!anyTabActive && e.NewItems.Cast<ITabItemView>().Any())
				{
					e.NewItems.Cast<ITabItemView>().OrderBy(it => it.Index).First().IsActive = true;
				}
			}
		}

		protected override IRegion CreateRegion()
		{
			return new TabsRegion(_regionTarget);
		}

		public class TabsRegion : Region
		{
			private readonly TabControl _tabControl;

			public TabsRegion(TabControl tabControl)
			{
				_tabControl = tabControl;
			}

			public override IViewsCollection ActiveViews
			{
				get
				{
					var items = _tabControl?.SelectedItem != null
						? new[] {_tabControl?.SelectedItem}.Cast<TabItem>()
						: new List<TabItem>();
					var partialResult = new ObservableCollection<ItemMetadata>(
						items.Select(it => new ItemMetadata(it)));
					return new ViewsCollection(partialResult, it => true);
				}
			}

			public override void Deactivate(object view)
			{
				base.Deactivate(view);
				var tab = (TabItem) view;
				tab.IsSelected = false;
			}

			public override void Activate(object view)
			{
				base.Activate(view);
				var tab = (TabItem)view;
				tab.IsSelected = true;
			}

			public override object GetView(string viewName)
			{
				Console.WriteLine("TEST");
				return base.GetView(viewName);
			}
		}
	}
}
