# mvvm-example
To an end user this application is simply a screen capture tool with questionable UI/UX decisions. In actualitty, this project serves as a nontrivial prototype and personal reference, demonstrating the Model View ViewModel (MVVM) pattern, featuring a stateful UI and live language switching.

##Features

###Live Language Switching

By clicking `Tools > Change the Language > [Language]` the user can change the language at anytime and the UI strings will update without restarting the application. This is done "MVVM style" using the `LocalizedStrings` ViewModel,  `strings.*.resx` resource files and binding in the `xaml` files shown below.

```
<Label Content="{Binding LocalizedResources.ScreenCaptureTitle, Source={StaticResource LocalizedStrings}}" />
```

You will notice that not all strings are localized depending on language. This is intentional, as it demonstratas the default fall backs work. All translations are done by using Google translate, as I know absolutly no Russian or Japanese, I hope there is nothing accidentally offensive here...

###Stateful UI

This appication demonstrates the use of views as states. Every View control has a ViewModel inheriting from `StateViewModel` and must implement 6 abstract methods that will be used by the `ContainerViewModel`.

####`NextView and BackView`

This is likely the most complicated method; however, it is still pretty straight forward. This method simply creates the new control or "state" that will replace the current one when the button is clicked. As demonstrated below, creating the new view may involve setting some fields in the View's ViewModel.

```
public override UserControl NextView
{
  get
  {
    var v = new AreaSelectControl();
    var viewModel = ((AreaSelectViewModel)v.DataContext);
    viewModel.Screen = SelectedScreen;
    return v;
  }
}
```

####`NextEnabled and BackEnabled`

Simply set the conditions for the particular buttons to be enabled.

```
public override bool NextEnabled
{
  get { return SelectedScreen != null; }
}
```

####`NextMessage and BackMessage`

Just returns the text to be displayed on the buttons.

##Design Decisions

###Minimal Branching

For better or worse, I took the somewhat "pythonic" approach and used this as en excercise to avoid `if` statements. Checking if a value is null just so you can throw an exception seems a little silly in a lot of cases.

###No Code Behind

There is no additional code in any of the `*.xaml.cs` classes as I feel that tends to violate MVVM. Doing all of this logic in the ViewModels makes unit testing trivial.

###No Duplication

This code goes through lengths to ensure changes need only be made in one place and they will propogate through the rest of the project. 

####Available Cultures

Rather then creating a static collection of cultures I support, I generate one by looking at what resource files exist.

```
private static readonly ReadOnlyObservableCollection<CultureInfo> _availableCultures
    = new ReadOnlyObservableCollection<CultureInfo>(
      new ObservableCollection<CultureInfo>(
          CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Where(cultureInfo =>  new ResourceManager(typeof(strings)).GetResourceSet(cultureInfo, true, false) != null)));
```

####Screen Capturers

This uses reflection to find all subclasses of the type `ScreenCapturer` and offer them as choices to the user. While this is  not always good practice, it does allow the coder to create a new `ScreenCapturer` child class and have it available without making any additional changes.

```
private static readonly ReadOnlyObservableCollection<Type> _capturers
    = new ReadOnlyObservableCollection<Type>(
      new ObservableCollection<Type>(
          typeof(ScreenCapturer).Assembly.GetTypes().Where(t => t.IsSubclassOf(typeof(ScreenCapturer)))));
```
