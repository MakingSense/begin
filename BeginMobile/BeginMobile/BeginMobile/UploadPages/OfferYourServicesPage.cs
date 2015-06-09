using BeginMobile.Services.DTO;
using ImageCircle.Forms.Plugin.Abstractions;
using Xamarin.Forms;

namespace BeginMobile.UploadPages
{
    public class OfferYourServices : ContentPage
    {
        private readonly StackLayout _mainStackLayout;
        private Picker _pickerCarrier;

        private const string ImaDesigner = "I am a Web Designer";
        private const string ImaTeacher = "I am a Teacher";
        private const string ImaPainter = "I am a Painter";
        private const string ImaStudent = "I am a Student";
        private const string ImaEngineer = "I am a Engineer";
        private const string Cancel = "Cancel";

        private Button _buttonOkReady;
        private StackLayout _stackLayoutButtons;
        private Label _labelWhatDoYouDo;
        private Label _labelServicesSubTitle;
        private TapGestureRecognizer _tapGestureRecognizer;
        private Button _buttonSelectFromList;
        private Label _labelChangeSubTitle;

        public OfferYourServices()
        {
            var user = (LoginUser)BeginApplication.Current.Properties["LoginUser"];
            Style = BeginApplication.Styles.PageStyle;
            BackgroundColor = BeginApplication.Styles.UploadBackgroundColor;

            _tapGestureRecognizer = new TapGestureRecognizer()
            {
                NumberOfTapsRequired = 1,
            };

            _tapGestureRecognizer.Tapped += async (s, e) =>
            {
                DisplayOptions();
            };

            _labelWhatDoYouDo = new Label
                                   {
                                       Text = "What do you do?",
                                       Style = BeginApplication.Styles.TitleStyle,
                                       XAlign = TextAlignment.Center
                                   };

            _labelServicesSubTitle = new Label
            {
                Text = "I am a Web Designer",
                Style = BeginApplication.Styles.SubtitleStyle,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = true
            };


            _labelChangeSubTitle = new Label
            {
                Text = "Change",
                Style = BeginApplication.Styles.SubtitleStyle,
                XAlign = TextAlignment.Center,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                IsVisible = false
            };
            _labelChangeSubTitle.GestureRecognizers.Add(_tapGestureRecognizer);

            var stackLayoutTitleSubTitle = new StackLayout()
            {
                Padding = new Thickness(0, 20, 0, 50),
                Children =
                {
                    _labelWhatDoYouDo,
                    _labelServicesSubTitle,
                    _labelChangeSubTitle
                }
            };

            var imageCarrier = new CircleImage
                              {
                                  Source = BeginApplication.Styles.CompleteJobIcon,
                                  Style = BeginApplication.Styles.CircleImageUpload,
								  HorizontalOptions = LayoutOptions.CenterAndExpand,
                              };

            var stackLayoutPicture = new StackLayout()
            {
                BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                Spacing = 5,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Padding = new Thickness(0, 20, 0, 0),
                Children =
                {
                    imageCarrier,
                }
            };

            _pickerCarrier = new Picker
                               {
                                   Items =
                                   {
                                       ImaDesigner,
                                       ImaTeacher,
                                       ImaPainter,
                                       ImaStudent
                                   },
                                   Title = "Select Your Profession",
                                   
                                   Style = BeginApplication.Styles.PickerStyle,
                                   BackgroundColor = Color.FromHex("A6A6A6"), 
                                   
                               };
            _pickerCarrier.SelectedIndexChanged += async (s, e) =>
            {

            };

            _buttonOkReady = new Button
                                {
                                    Text = "Ok, I'm Ready",
                                    Style = BeginApplication.Styles.UploadLinkLabelButton,
                                    BackgroundColor = Color.Transparent,
                                    HorizontalOptions = LayoutOptions.CenterAndExpand,
                                    FontSize = 16,
                                    IsVisible = false,
                                };

            

            _buttonOkReady.Clicked += async (s, e) =>
            {
                BeginApplication.CurrentBeginApplication.ShowMainPage(user);
            };

            var gridMain = new Grid()
            {
                Padding = new Thickness(0, 0, 0, 20),
                BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                RowSpacing = 6,
                RowDefinitions =
                {
                    new RowDefinition(){Height = GridLength.Auto},
                    new RowDefinition(){Height = GridLength.Auto}
                }
            };

            _buttonSelectFromList = new Button
            {
                Text = "Select from List",
                Style = BeginApplication.Styles.DefaultButton,
            };
            _buttonSelectFromList.Clicked += async (s, e) =>
            {
                DisplayOptions();
            };

            _stackLayoutButtons = new StackLayout()
            {
                Spacing = 10,
                Padding = new Thickness(0, 20, 0, 0),
                HorizontalOptions = LayoutOptions.FillAndExpand,
                Children =
                {
                    _buttonSelectFromList
                }
            };

            var stackCircleButtons = new StackLayout()
            {
                Orientation = StackOrientation.Horizontal,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                Children =
                {
                    BlackCircle(),
                    GreenCircle()
                }
            };

            gridMain.Children.Add(stackLayoutPicture, 0, 1);
            gridMain.Children.Add(stackLayoutTitleSubTitle, 0, 2);
            gridMain.Children.Add(_stackLayoutButtons, 0, 3);
            gridMain.Children.Add(_buttonOkReady, 0, 4);
            //gridMain.Children.Add(_pickerCarrier, 0, 5);

            _mainStackLayout = new StackLayout
                               {
                                   HorizontalOptions = LayoutOptions.Center,
                                   //BackgroundColor = BeginApplication.Styles.PageContentBackgroundColor,
                                   Children =
                                   {
                                       gridMain,stackCircleButtons
                                   },
                                   Padding =
                                       Device.Idiom == TargetIdiom.Phone
                                           ? new Thickness(32, Device.OnPlatform(20, 20, 20), 32, 10)
                                           : new Thickness(32, Device.OnPlatform(40, 40, 40), 32, 10)
                               };

            //TODO:chage this code for services api to profession            
            //save the harcode profession for recovery in the Profile ME this is temporaly
            BeginApplication.SelectedUserProfession = _labelServicesSubTitle.Text;

            var backgroundImage = new Image
            {
                Source = ImageSource.FromFile(BeginApplication.Styles.DefaultLoginBackgroundImage),
                Aspect = Aspect.Fill,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            };
            var relativeLayout = new RelativeLayout();
            relativeLayout.Children.Add(backgroundImage,
               xConstraint: Constraint.Constant(0),
               yConstraint: Constraint.Constant(0),
               widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
               heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));

            relativeLayout.Children.Add(_mainStackLayout,
              xConstraint: Constraint.Constant(0),
              yConstraint: Constraint.Constant(0),
              widthConstraint: Constraint.RelativeToParent((parent) => { return parent.Width; }),
              heightConstraint: Constraint.RelativeToParent((parent) => { return parent.Height; }));
            Content = relativeLayout;

            //SizeChanged +=(sender,e)=> ChangePadding(this);

        }
        private void ChangePadding(Page page)
        {
            _mainStackLayout.Padding = page.Width > page.Height
                ? new Thickness(page.Width * 0.01, page.Height * 0.15, page.Width * 0.01, page.Height * 0.01)
                : new Thickness(page.Width * 0.01, page.Height * 0.25, page.Width * 0.01, page.Height * 0.01);
        }

        private void RetrieveCarrierOptionSelected(out string carrier)
        {
            var carrierSelectedIndex = _pickerCarrier.SelectedIndex;

            carrier = carrierSelectedIndex == -1
                ? null
                : _pickerCarrier.Items[carrierSelectedIndex];
        }

        private async void DisplayOptions()
        {
            string action = await DisplayActionSheet(
                    null,
                    Cancel,
                    null,
                    ImaDesigner,
                    ImaTeacher,
                    ImaPainter,
                    ImaEngineer,
                    ImaStudent
                    );

            switch (action)
            {
                case ImaDesigner:
                    _labelWhatDoYouDo.Text = ImaDesigner;
                    _labelServicesSubTitle.IsVisible = false;
                    _labelChangeSubTitle.IsVisible = true;
                    _buttonOkReady.IsVisible = true;
                    _buttonSelectFromList.IsVisible = false;
                    BeginApplication.SelectedUserProfession = _labelWhatDoYouDo.Text;
                    break;
                case ImaTeacher:
                    _labelWhatDoYouDo.Text = ImaTeacher;
                    _labelServicesSubTitle.IsVisible = false;
                    _labelChangeSubTitle.IsVisible = true;
                    _buttonOkReady.IsVisible = true;
                    _buttonSelectFromList.IsVisible = false;
                    BeginApplication.SelectedUserProfession = _labelWhatDoYouDo.Text;
                    break;
                case ImaPainter:
                    _labelWhatDoYouDo.Text = ImaPainter;
                    _labelServicesSubTitle.IsVisible = false;
                    _labelChangeSubTitle.IsVisible = true;
                    _buttonOkReady.IsVisible = true;
                    _buttonSelectFromList.IsVisible = false;
                    BeginApplication.SelectedUserProfession = _labelWhatDoYouDo.Text;
                    break;
                case ImaEngineer:
                    _labelWhatDoYouDo.Text = ImaEngineer;
                    _labelServicesSubTitle.IsVisible = false;
                    _labelChangeSubTitle.IsVisible = true;
                    _buttonOkReady.IsVisible = true;
                    _buttonSelectFromList.IsVisible = false;
                    BeginApplication.SelectedUserProfession = _labelWhatDoYouDo.Text;
                    break;
                case ImaStudent:
                    _labelWhatDoYouDo.Text = ImaStudent;
                    _labelServicesSubTitle.IsVisible = false;
                    _labelChangeSubTitle.IsVisible = true;
                    _buttonOkReady.IsVisible = true;
                    _buttonSelectFromList.IsVisible = false;
                    BeginApplication.SelectedUserProfession = _labelWhatDoYouDo.Text;
                    break;
                case Cancel:
                    return;
                    break;
            }
        }

        private Image BlackCircle()
        {
            return new Image()
            {
                Source = BeginApplication.Styles.CompleteBlackCircle
            };
        }

        private Image GreenCircle()
        {
            return new Image()
            {
                Source = BeginApplication.Styles.CompleteGreenCircle,
            };
        }
    }
}