using Xamarin.Forms;

namespace DuboisAnke_Project.Behaviours
{
    public class MaxLengthValidatorBehaviour : Behavior<Entry>
   {
       protected override void OnAttachedTo(Entry bindable)
       {
           bindable.TextChanged += OnBindableTextChanged;
           base.OnAttachedTo(bindable);
       }

       protected override void OnDetachingFrom(Entry bindable)
       {
           bindable.TextChanged += OnBindableTextChanged;
           base.OnDetachingFrom(bindable);
       }

       void OnBindableTextChanged(object sender, TextChangedEventArgs args)
       {
           if (args.NewTextValue.Length > 25)
           {
               ((Entry)sender).TextColor = Color.DarkRed;
                App.Current.MainPage.DisplayAlert("Info", "Please keep the title under 25 characters!", "Ok");
           }
       }

    }
}
