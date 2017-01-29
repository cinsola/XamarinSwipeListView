# XamarinSwipeListView
##Basic Setup
After cloning, initialize the SwipeListView:
```
<xam:SwipeListView ItemsSource="{Binding ListItems}">
			<xam:SwipeListView.ItemTemplate>
				<DataTemplate>
					<ViewCell>
						<ViewCell.View>
							<xam:SwipeItemView BoundItem="{Binding .}" x:Name="loopedElement">
								<xam:SwipeItemView.MainContent>
										<!-- Content !-->
								</xam:SwipeItemView.MainContent>
								<xam:SwipeItemView.SwipeRightContent>
										<!-- Content !-->
								</xam:SwipeItemView.SwipeRightContent>
								<xam:SwipeItemView.SwipeLeftContent>
										<!-- Content !-->
								</xam:SwipeItemView.SwipeLeftContent>
							</xam:SwipeItemView>
						</ViewCell.View>
					</ViewCell>
				</DataTemplate>
			</xam:SwipeListView.ItemTemplate>
		</xam:SwipeListView>
```
As you can imagine `xam:SwipeItemView` define the template of the Swipe element. You can define the template of the box behind too customizing `SwipeLeftContent` and `SwipeRightContet`.
`SwipeLeftContent`, `SwipeRightContet` and `MainContent` accepts bounded `View`.
> See `MainPage.xaml` for a complete example

##Events and Custom properties
`xam:SwipeItemView` defines some events and properties:
- `SwipeLeftCompleted`
- `SwipeRightCompleted`
- `DismissSwipeBefore` dismiss the swipe if the gesture haven't completed the action for the setted number (from 0.1 to 1)
- `DismissSwipe` fired when swipe is dismissed
- `SwipeDuration` animation duration for the swipe to complete the action after the gesture is completed (`uint`)
- `ChangeOpacity` set to `true` to animate opacity. Default is `false`.

##Contributions and known issues
Due to this bug: https://bugzilla.xamarin.com/show_bug.cgi?id=49658 - the method to remove an element from the List is a bit trivial. Use:
```
		private void RemoveElement(object sender, EventArgs e)
		{
			SwipeItemView itemView = (sender as T).CommandParameter as SwipeItemView;
			swipeListView.PreventXamarinBug();
			(this.BindingContext as Q).ListItems.Remove((U)(itemView.BoundItem));
		}
```
>See `Main.xaml.cs` for a working example

### iOs project needed :shipit:
Currently this Xamarin Forms SwipeListView works only on Android. I'm looking for a contributor who would code the iOs project.
