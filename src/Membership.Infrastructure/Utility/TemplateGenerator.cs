using System.Text;

namespace Membership.Infrastructure.Utility;

public static class TemplateGenerator
{
    public static string GetHTMLString(string membershipNo, string date, string fullName, 
	    string district, string mandalam, string panchayath, string emirate, string area, string collectedBy )
    {  
        var sb = new StringBuilder();
        sb.Append(@$"<html>
					<head>
					</head>
					<body>
						<div class='id-card-holder'>
							<div class='id-card'>
								<div class='header'>
									<img src='http://member.uaekmcc.com/_next/image?url=%2Fimages%2Fmembership_card_header.png&w=128&q=75'>
								</div>
								<div class='photo'>
								</div>
								<h2>Name : {fullName}</h2>
								<div class='qr-code'>
								</div>
								<h3>Membership No : <strong> {membershipNo}</strong></h3>
								<hr>
								<p>Registration Date : {date}<p>
								<p>District {district}</p>
								<p>Mandalam {mandalam}</p>
								<p>Municipality/Panchayath {panchayath}</p>
								<p>Emirates {emirate}, Area {area}</p>
								<p>Agent : {collectedBy}</p>
							</div>
						</div>
					</body>
				</html>");
        return sb.ToString();
    }
}