using System;
using Xamarin.Forms;
using System.Collections.Generic;
using Hanselman.Portable.Views;
using Hanselman.Portable.Helpers;

namespace Hanselman.Portable
{
  public class HomeView : MasterDetailPage
  {
    private HomeViewModel ViewModel
    {
      get { return BindingContext as HomeViewModel; }
    }
    HomeMasterView master;
    private Dictionary<MenuType, NavigationPage> pages;
    public HomeView()
    {
      pages = new Dictionary<MenuType, NavigationPage>();
      BindingContext = new HomeViewModel();

      Master = master = new HomeMasterView(ViewModel);

      var homeNav = new NavigationPage(master.PageSelection)
      {
        BarBackgroundColor = Color.FromHex("#3498DB"),
        BarTextColor = Color.White
      };
      Detail = homeNav;

			pages.Add(MenuType.Events, homeNav);

      master.PageSelectionChanged = async (menuType) =>
      {

        if (Detail != null && Device.OS == TargetPlatform.WinPhone)
        {
          await Detail.Navigation.PopToRootAsync();
        }

        NavigationPage newPage;
        if (pages.ContainsKey(menuType))
        {
          newPage = pages[menuType];
        }
        else
        {
          newPage = new NavigationPage(master.PageSelection)
          {
            BarBackgroundColor = Color.FromHex("#3498DB"),
            BarTextColor = Color.White
          };
          pages.Add(menuType, newPage);
        }
        Detail = newPage;
        Detail.Title = master.PageSelection.Title;
        if(Device.Idiom != TargetIdiom.Tablet)
          IsPresented = false;
      };

      this.Icon = "slideout.png";
    }
  }


  public class HomeMasterView : BaseView
  {
    public Action<MenuType> PageSelectionChanged;
    private Page pageSelection;
		private MenuType menuType = MenuType.Events;
    public Page PageSelection
    {
      get { return pageSelection; }
      set
      {
        pageSelection = value;
        if (PageSelectionChanged != null)
          PageSelectionChanged(menuType);
      }
    }
    private Page about, blog, twitter, hanselminutes, ratchet, developerlife, events;
    public HomeMasterView(HomeViewModel viewModel)
    {
      this.Icon = "slideout.png";
      BindingContext = viewModel;


      var layout = new StackLayout { Spacing = 0 };

      var label = new ContentView
      {
        Padding = new Thickness(10, 36, 0, 5),
        BackgroundColor = Color.Transparent,
        Content = new Label
        {
          Text = "MENU",
          FontSize = Device.GetNamedSize(NamedSize.Medium, typeof(Label))
        }
      };

      layout.Children.Add(label);

      var listView = new ListView();

      DataTemplate cell = null;

      if (Device.OS == TargetPlatform.Windows)
      {
        cell = new DataTemplate(typeof(FancyListCell));
        this.BackgroundColor = Color.FromHex("3498DB");
      }
      else
      {
        cell = new DataTemplate(typeof(ListImageCell));
        cell.SetBinding(TextCell.TextProperty, HomeViewModel.TitlePropertyName);
        cell.SetBinding(ImageCell.ImageSourceProperty, "Icon");
      }



      listView.ItemTemplate = cell;

      listView.ItemsSource = viewModel.MenuItems;
      if (about == null)
        about = new AboutPage();

      PageSelection = about;
      //Change to the correct page
      listView.ItemSelected += (sender, args) =>
      {
        var menuItem = listView.SelectedItem as HomeMenuItem;
        menuType = menuItem.MenuType;
        switch (menuItem.MenuType)
        {

			case MenuType.Events:
				if (events == null) {
					events = new EventListPage();
				}

				PageSelection = events;
					break;

				case MenuType.CreateEvent:
					if (events == null) {
						events = new EventListPage();
					}

					PageSelection = events;
					break;
        }
      };

      listView.SelectedItem = viewModel.MenuItems[0];
      layout.Children.Add(listView);

      Content = layout;
    }
  }

}

