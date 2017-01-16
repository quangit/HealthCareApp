# Alliance Picker

####Alliance Picker is a customizable and user friendly Picker control.

**Picker Types**

- List
- Date
- Time
- DateTime
- Information
- Login

**Here is an example**

    using AllianceCustomPicker;   
	var items = new List<string> {
          "Alabama", "Alaska", "Arizona", "Arkansas", 
          "California", "Colorado", "Connecticut"
       };

	var picker = new AlliancePicker (this);
	picker.PlainPickerItems = items;
	picker.SourceField = TextBox2;
	picker.Type = PickerType.List;
	picker.HeaderTitle = "Choose State";
	picker.Show ();

- User can customize the colors, fonts.



