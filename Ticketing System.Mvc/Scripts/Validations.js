/// <reference path="validation.js" />
$(document).ready(function () {
   
    try {

        //custom rule for dropdownlist
        jQuery.validator.addMethod("notEqual", function (value, element, param) {
            return this.optional(element) || value !== param;
        }, "Please choose a value!");

        //custom validations
        jQuery.validator.addMethod('phone', function (value, element) {
            return this.optional(element) || /^\d{3}-\d{3}-\d{4}$/.test(value);
        }, "Please enter a valid phone number");
    } catch (ex)
    { }
    var pagename = window.location.pathname.toLowerCase();
    
    if (pagename == "/tickets/create") {
        $("form").removeAttr('novalidate');
        $("form").validate({
            rules: {
                Title: {
                    required: true
                },
                Description: { required: true }
                , DueDate: { required: true },
                AssigndedDate: { required: true },
                RefereedTo: { required: true },
                ProjectID: { required: true },
                AssignedTo: { required: true },
                PriorityID: { required: true },
                Task_Status: { required: true },
                TypeID:{ required: true }
            },
            messages: {
                Title: "Title required",
                Description: "Description required",
                DueDate: "Due Date required",
                AssigndedDate: "Assigned Date required",
                RefereedTo:"Please select user",
                ProjectID: "Please select user",
                AssignedTo: "Please select user",
                PriorityID:"Please select priority",
                Task_Status:"Please select Task_Status",
                TypeID: "Please select Task_Status"
            },

        });
        
    }
   else if (pagename == "/tickets/edit") {
        $("form").removeAttr('novalidate');
        $("form").validate({
            rules: {
                Title: {
                    required: true
                },
                Description: { required: true }
                , DueDate: { required: true },
                AssigndedDate: { required: true },
                RefereedTo: { required: true },
                ProjectID: { required: true },
                AssignedTo: { required: true },
                PriorityID: { required: true },
                Task_Status: { required: true },
                TypeID: { required: true }
            },
            messages: {
                Title: "Title required",
                Description: "Description required",
                DueDate: "Due Date required",
                AssigndedDate: "Assignded Date is required",
                RefereedTo: "Please select user",
                ProjectID: "Please select user",
                AssignedTo: "Please select user",
                PriorityID: "Please select priority",
                Task_Status: "Please select Task_Status",
                TypeID: "Please select Task_Status"
            },

        });

    }
    else if (pagename == "/users/create") {
        $("form").removeAttr('novalidate');
        $("form").validate({
            rules: {
                FirstName: {
                    required: true
                },
                LastName: { required: true }
                , Email: { required: true },
                MobileNumber: { required: true },
                Password: { required: true }
            },
            messages: {
                FirstName: "First Name required",
                LastName: "Last Name required",
                Email: "Email required",
                MobileNumber: "Mobile Number required",
                Password:"Role is required"
            },

        });

    }
    else if (pagename == "/projects/create") {
        $("form").removeAttr('novalidate');
        $("form").validate({
            rules: {
                Name: {
                    required: true
                },
                Description: { required: true },
                ShortName: { required: true },
                Duration: { required: true },
                SignUpDate: { required: true },
                StartDate: { required: true },
                ProposedEndDate: { required: true },
                PManagerID: { required: true },
                ClientID:{ required: true }
            },
            messages: {
                Name: "Name required",
                Description: "Description required",
                ShortName: "Short Name required",
                Duration: "Duration required",
                SignUpDate: "SignUp Date required",
                StartDate: "Start Date required",
                ProposedEndDate: "Proposed EndDate required",
                PManagerID: "Project Manager is required",
                ClientID: "Client is required"
            },

        });//

    }
    else if (pagename == "/projects/edit") {
        $("form").removeAttr('novalidate');
        $("form").validate({
            rules: {
                Name: {
                    required: true
                },
                Description: { required: true },
                ShortName: { required: true },
                Duration: { required: true },
                SignUpDate: { required: true },
                StartDate: { required: true },
                ProposedEndDate: { required: true }
            },
            messages: {
                Name: "Name required",
                Description: "Description required",
                ShortName: "ShortName required",
                Duration: "Duration required",
                SignUpDate: "SignUpDate required",
                StartDate: "StartDate required",
                ProposedEndDate: "ProposedEndDate required"
            },

        });///Projects/Edit

    }
    else if (pagename == "create") {
        //  //  //alert('Page : ' + pagename);

        //get controller name
        var controllername = window.location.pathname.toLocaleLowerCase();

        if (controllername.indexOf('project') >= 0)
        {
            // create project

            $("form").validate({
                rules: {

                    Name: { required: true }, Description: { required: true }, ShortName: { required: true }

                },
                messages: {

                    Name: "Series Name required",
                    Description: "Description required",
                    ShortName: "Image  required"


                },
                success: function () {

                    $("#loadingDiv").hide();
                }
            });

        }

        $("form").validate({
            rules: {

                txtseriesname: { required: true }, txtseriesdescription: { required: true }, fileseriesimage: { required: true }

            },
            messages: {

                txtseriesname: "Series Name required",
                txtseriesdescription: "Description required",
                fileseriesimage: "Image  required"


            },
            success: function () {

                $("#loadingDiv").hide();
            }
        });
    }
    else if (pagename == "editseries") {
      //  //  //alert('Page : ' + pagename);
        $("#editseries").validate({
            rules: {

                Name: { required: true }  , Description: { required: true }

            },
            messages: {

                Name: "Name required",
                Description: "Description  required",
            },
            success: function () {
                HideProgressBar();
            }, failure: function () {
                ShowProgressBar();
            }, submitHandler: function () {
                ShowProgressBar();
                $(this).valid();
            }
        });
    }//ChurchSignUpProfile
    else if (pagename == "churchsignupprofile") {
        ////  //alert('Page : ' + pagename);
        $("form").validate({
            rules: {

                Name: { required: true }, Author: { required: true }, Description: { required: true }, ContactPersonName: { required: true }, ContactPersonEmail: { required: true, email: true }, Username: { required: true }, ContactPersonPhoneNum: { required: true, phone: true }, Password: { required: true }, cpassword: { required: true, equalTo: "#Password" }, PhoneNum: { required: true, phone: true }, Email: { required: true, email: true }, Address: { required: true }

            },
            messages: {

                Name: "Name required",
                Author: "Author required",
                Description: "Description  required",
                ContactPersonName: "ContactPersonName  required",
                ContactPersonEmail: {
                    required: "ContactPersonEmail required",
                    email: "Invalid Email"
                },
                Username: "Username required",
                ContactPersonPhoneNum: {
                    required: "ContactPersonPhoneNum required",
                    phone: "Invalid Phone Number"
                },
                Password: "Password required", 
                cpassword: "cpassword required",
                PhoneNum: {
                    required: "PhoneNum required",
                    phone: "Invalid Phone Number"
                },
                Email: {
                    required: "Email required",
                    email: "Invalid Email"
                },
                Address: "Address required",

            },
            success: function () {

                $("#loadingDiv").hide();
            }
            
        });

    }


    else if (pagename == "churchsignupconfiguration") {
       // //  //alert('in validations, church signup onfig js page');
        $("form").validate({
            ignore: [],
            rules: {

                txtcolorscheme: { required: true }, splashimage: { required: true }, bannerstring: { required: true }

            },
            messages: {

                txtcolorscheme: "Color Scheme  required",
                splashimage: "Splash Image required",
                bannerstring: "Banner  required"
               
            },
            success: function () {

                $("#loadingDiv").hide();
            }

        });

    }
        //EditSermon
    else if (pagename == "editsermon") {
       // //  //alert('Page : ' + pagename);
        $("form").validate({
            rules: {

                Name: { required: true }, Author: { required: true }, Description: { required: true }, Updated: { required: true }, ddlseries: { required: true }

            },
            messages: {

                Name: "Name required",
                Author: "Author required",
                Description: "Description  required",
                Updated: "Updated  required",
                ddlseries: "ContactPersonEmail required"

            },
            success: function () {
                HideProgressBar();
            }, failure: function () {
                ShowProgressBar();
            }, submitHandler: function () {
                ShowProgressBar();
                $(this).valid();
            }
        });
    }
    else if (pagename == "forgotpassword") {

        $("form").validate({
            rules: {
                txtforgotpasswordemail: { required: true, email: true }

            },
            messages: {
                txtforgotpasswordemail: { required: "Email required", email: "Invalid Email Address" }
            },
            success: function () {

                $("#loadingDiv").hide();
            }
        });
    }
        //CreateEvent
    else if (pagename == "createevent") {

        $("form").validate({
            rules: {
                Name: { required: true }, Description: { required: true }, Starts: { required: true }, Ends: { required: true }

            },
            messages: {
                Name: { required: "Name required" },
                Description: { required: "Description required" },
                Starts: { required: "Starts required" },
                Ends: { required: "Endss required" }
            },
            failure: function () {

                $("#loadingDiv").hide();
            },
            success: function () {

                $("#loadingDiv").hide();
            }
        });
    }
        //EditEvent
    else if (pagename == "editevent") {

        $("form").validate({
            rules: {
                Name: { required: true }, Description: { required: true }, Starts: { required: true }, Ends: { required: true }

            },
            messages: {
                Name: { required: "Name required" },
                Description: { required: "Description required" },
                Starts: { required: "Starts required" },
                Ends: { required: "Endss required" }
            },
            success: function () {
                
                $("#loadingDiv").hide();
            }
        });
    }
        //CreateSermon
    else if (pagename == "createsermon") {
        
        $("#createsermonform").validate({
            rules: {
                SeriesId: { required: true }, Name: { required: true }, Author: { required: true }, Description: { required: true }, Updated: { required: true }, filesermonaudio: { required: true }

            },
            messages: {
                SeriesId: { required: "SeriesId required" },
                Name: { required: "Name required" },
                Author: { required: "Author required" },
                Description: { Description: "Endss required" },
                Updated: { Updated: "Endss required" },
                filesermonaudio: { filesermonaudio: "Endss required" }
            },
            success: function () {

                $("#loadingDiv").hide();
            }
        });

        

        $("#createseriesform").validate({
            rules: {
                txtseriesname: { required: true }, txtseriesdescription: { required: true }, filethumbnail: { required: true } 

            },
            messages: {
                txtseriesname: { required: "SeriesName required" },
                txtseriesdescription: { required: "SeriesDescription required" },
                filethumbnail: { required: "Thumbnail required" }              
            },
            success: function () {

                $("#loadingDiv").hide();
            }
        });
    }
        //ManageContent
    else if (pagename == "managecontent") {
      //  //  //alert('page' + pagename)
        $("form").validate({
            ignore: [],
            rules: {
                ColorScheme: { required: true }, splashimage: { required: true }, bannerstring: { required: true }
            },
            messages: {
                ColorScheme: { required: "ColorScheme required" },
                splashimage: { required: "Splash Image required" },
                bannerstring: { required: "Banner  required" }
            },
            success: function () {
                HideProgressBar();
            }, failure: function () {
                ShowProgressBar();
            }, submitHandler: function () {
                ShowProgressBar();
                $(this).valid();
            }
        });
    }
//EditProfile
    else if (pagename == "editprofile") {
           
        $("form").validate({
               rules: {
                   'ChurchInfo.Name': { required: true },
                   'ChurchInfo.Description': { required: true },
                   'ChurchInfo.PhoneNum': { required: true },
                   'ChurchInfo.Email': { required: true },
                   'ChurchInfo.Username': { required: true },
                   'ChurchInfo.Password': { required: true },
                   'ChurchInfo.Address': { required: true },
                   'ChurchInfo.ContactPersonName': { required: true },
                   'ChurchInfo.ContactPersonEmail': { required: true },
                   'ChurchInfo_ContactPersonPhoneNum': { required: true }
                   
        },
            messages: {
                'ChurchInfo.Name': { required: "Church Name required" },
                'ChurchInfo.Description': { required: "Description  required" },
                'ChurchInfo.Username': { required: "UserName required" },
                'ChurchInfo.Email': { required: "Email required" },
                'ChurchInfo.PhoneNum': { required: "PhoneNumber required" },
                'ChurchInfo.Password': { required: "Password required" },
                'ChurchInfo.Address': { required: "Address required" },
                'ChurchInfo.ContactPersonName': { required: "ContactPersonName required" },
                'ChurchInfo.ContactPersonEmail': { required: "ContactPersonEmail required" },
                'ChurchInfo.ContactPersonPhoneNum': { required: "ContactPersonPhoneNum required" }
                
            },
            success: function () {
                HideProgressBar();
            }, failure: function () {
                ShowProgressBar();
            }, submitHandler: function () {
                ShowProgressBar();
                $(this).valid();
            }
        });
    }

        //Createoffering
    else if (pagename == "createoffering") {

        $("form").validate({
            rules: {
                Name: { required: true }, Description: { required: true }, Starts: { required: true }, Ends: { required: true }

            },
            messages: {
                
                Name: { required: "Name required" },
                Description: { required: "Description required" },
                Starts: { required: "Start date required" },
                Ends: { required: "End date required" }
            },
            success: function () {

                $("#loadingDiv").hide();
            }
        });
    }

        //Edit offering
    else if (pagename == "editoffering") {

        $("form").validate({
            rules: {
                Name: { required: true }, Description: { required: true }, Starts: { required: true }, Ends: { required: true }

            },
            messages: {

                Name: { required: "Name required" },
                Description: { required: "Description required" },
                Starts: { required: "Start date required" },
                Ends: { required: "End date required" }
            },
            success: function () {

                $("#loadingDiv").hide();
            }
        });
    }


    function getPageName(url) {
        //  //alert('url is'+url);
        var index = url.lastIndexOf("/") + 1;
        if (index == 1) {
            return "Login";
        }
        var filenameWithExtension = url.substr(index);
        var filename = filenameWithExtension.split(".")[0]; // <-- added this line
        return filename;                                    // <-- added this line
    }
   
});