# XamarinSwipeListView

## Introduction
This is a result you can obtain:

![alt tag](http://cristianoinsola.it/blogcontents/gifSwipe.gif)


## Basic Setup
1. Download the nuget package:
```
Install-Package SwipeCollectionView
```
2. In you Android project, enable the collection view flag:
```csharp
protected override void OnCreate(Bundle savedInstanceState)
{
    //...
    base.OnCreate(savedInstanceState);
    global::Xamarin.Forms.Forms.SetFlags("CollectionView_Experimental");
    Xamarin.Essentials.Platform.Init(this, savedInstanceState);
    global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
    LoadApplication(new App());
}
```
3. In your forms project add a collection view. 
```xml
<xam:SwipeCollectionView ItemsSource="{Binding ListItems}" x:Name="swipeListView">
    <xam:SwipeCollectionView.ItemTemplate>
        <DataTemplate>
            <xam:SwipeItemView  x:Name="loopedElement"
                                   BoundItem="{Binding}"
                                   ChangeOpacity="True"
                                   SwipeRightCompleted="SwipeRightCompleted">
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
        </DataTemplate>
    </xam:SwipeCollectionView.ItemTemplate>
</xam:SwipeCollectionView>
```
As you can imagine `xam:SwipeItemView` define the template of the Swipe element. You can define the template of the box behind too customizing `SwipeLeftContent` and `SwipeRightContet`.
`SwipeLeftContent`, `SwipeRightContet` and `MainContent` accepts bounded `View`.
> See the `ExampleApp` for a complete example

## Events and Custom properties
`xam:SwipeItemView` defines some events and properties:
- `SwipeLeftCompleted`
- `SwipeRightCompleted`
- `DismissSwipeBefore` dismiss the swipe if the gesture haven't completed the action for the setted number (from 0.1 to 1)
- `DismissSwipe` fired when swipe is dismissed
- `SwipeDuration` animation duration for the swipe to complete the action after the gesture is completed (`uint`)
- `ChangeOpacity` set to `true` to animate opacity. Default is `false`.
