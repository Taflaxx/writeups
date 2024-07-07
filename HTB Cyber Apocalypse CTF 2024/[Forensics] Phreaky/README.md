#  Phreaky
In the shadowed realm where the Phreaks hold sway, A mole lurks within leading them astray. Sending keys to the Talents, so sly and so slick, A network packet capture must reveal the trick. Through data and bytes, the sleuth seeks the sign, Decrypting messages, crossing the line. The traitor unveiled, with nowhere to hide, Betrayal confirmed, they'd no longer abide.

## Solution

Looking through the provided `phreaky.pcapng` file, you can see a lot of SMTP traffic.
In total there are 15 emails, all of which contain a ZIP file attachment.
The ZIP files are password protected and the password is always included in the email body.
```
[...]
--=-=fbosNwJ3CADT01mLIW0zQjkRCcH2M0WNfVXJ=-=
Content-Type: text/plain; charset=us-ascii
Content-Disposition: inline
Content-ID: <20240306151313.FPlz0%caleb@thephreaks.com>

Attached is a part of the file. Password: yH5vqnkm7Ixa

--=-=fbosNwJ3CADT01mLIW0zQjkRCcH2M0WNfVXJ=-=
Content-Type: application/zip
Content-Transfer-Encoding: base64
Content-Disposition: attachment; 
 filename*0="882d61be23f7a3679ea0b86e6ea7e106dd1b1c1fd693075ba8a76bdba62";
 filename*1="66706.zip"
Content-ID: <20240306151313.RoykJ%caleb@thephreaks.com>

UEsDBAoACQAAAGZ3Zlhtggyo6QAAAN0AAAAWABwAcGhyZWFrc19wbGFuLnBkZi5wYXJ0OFVUCQAD
wIToZcCE6GV1eAsAAQToAwAABOgDAAAdQnVCaWFrDEjWzqeKKB2AMYPFi420ZNqxM1yPqH5JisD8
HNTACyex0E/ba1jNeoh/EbXy+7QIYZwB8Nw34uwQeXEeY2Fh46RBMBERIqI/jiT7YWorDyD+D+1j
dH/IBP1wDmvqtJbLTBz5dj2S+Om0GcvGpv7sSNa9shl0/FIvW3uOBorbQ8cpTh5D01vRxfFGw6v4
JBlGhwQLTGPS0EGxD6U/Uogrfwy23E7q+HI6cMPCOuVBTyMHiDj5itTTFKNyZYrAMQkX1OL07I3l
geTWK6IqlYlQO3rT4jPnNA+F+nh6fF0pwZCKMlBLBwhtggyo6QAAAN0AAABQSwECHgMKAAkAAABm
d2ZYbYIMqOkAAADdAAAAFgAYAAAAAAAAAAAAtIEAAAAAcGhyZWFrc19wbGFuLnBkZi5wYXJ0OFVU
BQADwIToZXV4CwABBOgDAAAE6AMAAFBLBQYAAAAAAQABAFwAAABJAQAAAAA=

--=-=fbosNwJ3CADT01mLIW0zQjkRCcH2M0WNfVXJ=-=--
```
The ZIP files all contain a part of a PDF file.
I then converted the base64 encoded ZIP files to a regular ZIP files and extracted the PDF files.
Then I used the Windows `copy` command to concatenate all the PDF files into one:

`copy /B phreaks_plan.pdf.* plan.pdf`

The assembled PDF file contains the flag:

`HTB{Th3Phr3aksReadyT0Att4ck}`