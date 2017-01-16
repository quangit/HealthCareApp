**Alliance Picker Provides following features**

- Custom Pickers
- User Friendly
- Custom Events

**Different modes in Alliance Picker**

- List
- Date
- Time
- DateTime
- Information
- Login

**How to use**

**Include the below namespace**

    using AllianceCustomPicker;

**List Mode**

- In this sample code ChooseBtn is an UIButton in sample application.
- Assign List of string items to populate a List.
- Specify the Destination Control TextField where to display the text.
- You can change the Title.

**Sample Code:**

    public override void ViewDidLoad() 
    {
    	base.ViewDidLoad();
    	ChooseBtn.TouchUpInside += (object sender, EventArgs e) => {
    		var items = new List<string> {
    		"Male", "Female"
    		};
    		var picker = new AlliancePicker (this);
    		picker.PlainPickerItems = items;
    		picker.SourceField = TextBox2;
    		picker.Type = PickerType.List;
    		picker.HeaderTitle = "Choose Gender";
    		picker.Show ();
    	};
    }

**Date Mode**

- Choose the type as Date.
- You can specify the seperator, date format, full month mode.
- Specify the Destination Control TextField where to display the text.
- You can change the Title.
- You can change colors.

**Sample Code**

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        ChooseBtn.TouchUpInside += (object sender, EventArgs e) => {
            var picker1 = new AlliancePicker (this);
            picker1.SourceField = TextBox1;
            picker1.Type = PickerType.Date;
            picker1.DateFormat = SelectDateFormat.DMY;
            picker1.Seperator = "-";
            picker1.BorderColor = (UIColor.Brown).CGColor;
            picker1.BackgrondColor = UIColor.Brown;
            picker1.ChooseButtonColor = UIColor.Blue;
            picker1.PlusMinusColor = UIColor.White;
            picker1.IsFullMonthRequired = true;
            picker1.HeaderTitle = "Choose Date";
            picker1.Show ();
        };
    }

**Time Mode**

- Choose the type as Time.
- Specify the Destination Control TextField where to display the text.
- You can change the Title.
- You can change colors.

**Sample Code**

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        ChooseBtn.TouchUpInside += (object sender, EventArgs e) => {
            var picker2 = new AlliancePicker (this);
            picker2.SourceField = TextBox3;
            picker2.Type = PickerType.Time;
            picker2.BorderColor = (UIColor.Magenta).CGColor;
            picker2.BackgrondColor = UIColor.Magenta;
            picker2.ChooseButtonColor = UIColor.Blue;
            picker2.PlusMinusColor = UIColor.Black;
            picker2.HeaderTitle = "Choose Time";
            picker2.Show ();
        };
    }

**Date and Time Mode**

- Choose the type as DateTime
- Specify the Destination Control TextField where to display the text
- You can change the Title
- You can change colors

**Sample Code**

    public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        ChooseBtn.TouchUpInside += (object sender, EventArgs e) => {
            var picker2 = new AlliancePicker (this);
            picker2.SourceField = TextBox3;
            picker2.Type = PickerType.DateTime;
            picker2.BorderColor = (UIColor.Magenta).CGColor;
            picker2.BackgrondColor = UIColor.Magenta;
            picker2.ChooseButtonColor = UIColor.Blue;
            picker2.PlusMinusColor = UIColor.Black;
            picker2.HeaderTitle = "Choose Time";
            picker2.Show ();
        };
    }

**Information Mode**

- Choose the type as Info
- You can change the Title
- Specify the text to show

**Sample Code**

	public override void ViewDidLoad()
    {
        base.ViewDidLoad();
        ChooseBtn.TouchUpInside += (object sender, EventArgs e) => {
            var picker2 = new AlliancePicker (this);
            picker2.Type = PickerType.Plain;
            picker2.HeaderTitle = "Sample Information";
            picker2.PlainDescription = "Sample Text";
            picker2.Show ();
        };
    }

**Login Mode**

- Choose the type as Login
- You can change the Title
- You can modify the Login Button Title
- You can specify the username and password max length
- Extend your custom logic in the LoginClicked event

**Sample Code**

	public override void ViewDidLoad()
    {
        base.ViewDidLoad();

        ChooseBtn.TouchUpInside += (object sender, EventArgs e) => {
            var picker2 = new AlliancePicker (this);
            picker2.Type = PickerType.Login;
            picker2.LoginNameString = "Authenticate";
            picker2.UserNameMaxLength = 8;
            picker2.PasswordMaxLength = 8;
            picker2.LoginClicked += (object s1, EventArgs e1) => {
                UIAlertView view = new UIAlertView ("Login", "Login
                    Successfull", null, "Ok", null);
                    view.Show ();
            };
            picker2.Show ();
        };
    }

**To change colors and fonts use the below properties**

1. Picker Background Color

        <<PickerObject>>.PickerColor = <<User Defined Color>>

2. Picker Title Color

    	<<PickerObject>>.PickerTitleColor = <<User Defined Color>>

3. Picker List Elements Color

        <<PickerObject>>.ListTextColor = <<User Defined Color>>

4. Choose Button Color

		<<PickerObject>>.ChooseButtonColor = <<User Defined Color>>

5. Date, Time and DateTime types Border, Text and Background Color

		<<PickerObject>>.BackgrondColor = <<User Defined Color>>
   		<<PickerObject>>.BorderColor = (<<User Defined Color>>).CGColor
    	<<PickerObject>>.PlusMinusColor = <<User Defined Color>>

6. To apply font for Picker Control

		<<PickerObject>>.PickerFont = <<User Defined Font>>