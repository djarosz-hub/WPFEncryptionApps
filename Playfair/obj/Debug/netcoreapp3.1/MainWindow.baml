   M S B A M L   `   `   `  ����C  ?Playfair, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null#clr-namespace:PlayfairPlayfair  5   	       Playfair.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   (localclr-namespace:Playfair     5   	     � Title$+  %Playfair cipher encrypter application��5   	   $	��450��67   $	��800��6D   .��5	      � ���	#FF85F5F3.+�5
   
   �� $��Playfair Cipher Encrypter��6   $��Center=�64   $��0,16,0,0q�6Q   $	��Top=�6c   $��48��6{   $	��344��6�   $��26(�6�   $��Center=�6�   �� 5   
   $��Text to encrypt:��6   $
��Left=�6+   $��
33,117,0,0q�6F   $	��Top=�6Z   $��16(�6r   �� 5   
   $
��Key:��6   $
��Left=�6   $��112,150,0,0q�6:   $	��Top=�6O   $��16(�6g   �� 5   
   $��Encrypted text:��6   $
��Left=�6*   $��
36,182,0,0q�6E   $	��Top=�6Y   $��16(�6q   �� 5   
   $��Text to decrypt��6   $
��Left=�6*   $��
36,278,0,0q�6E   $	��Top=�6Y   $��16(�6q   �� 5   
   $
��Key:��6   $
��Left=�6   $��112,312,0,0q�6:   $	��Top=�6O   $��16(�6g   �� 5   
   $��Decrypted text:��6   $
��Left=�6*   $��
34,345,0,0q�6E   $	��Top=�6Y   $��16(�6q   �� 5   
   $��	Encrypter��6   $��Center=�6$   $��0,64,0,0q�6A   $	��Top=�6S   $��24(�6k   �� 5   
   $��	Decrypter��6   $��Center=�6$   $	��Top=�6A   $��24(�6Y   $��	0,228,0,0q�6g   �� 5   
   -   5         ��Name$ EncryptInput��5      $��Center=�6(   $��Center=�6K   $
��Left=�6o   $��162,121,0,0q�6�   $�� ��6�    �� TextWrapping$
 Wrap=�6�   $	��Top=�6�   $	��410��6�   $��24��6�   �� 5   
   -   5        $ 
EncryptKey��5      $��Center=�6&   $��Center=�6I   $
��Left=�6m   $��162,154,0,0q�6�   $�� ��6�   $
 Wrap=�6�   $	��Top=�6�   $	��410��6�   $��24��6�   �� 5   
   -   5        $ EncryptOutput��5      $��Center=�6)   $��Center=�6L   $
��Left=�6p   $��162,186,0,0q�6�   $�� ��6�   $
 Wrap=�6�   $	��Top=�6�   $	��410��6�   $��24��6�   �� 5   
   -   5        $ DecryptInput��5      $��Center=�6(   $��Center=�6K   $
��Left=�6o   $��162,282,0,0q�6�   $�� ��6�   $
 Wrap=�6�   $	��Top=�6�   $	��410��6�   $��24��6�   �� 5   
   -   5        $ 
DecryptKey��5      $��Center=�6&   $��Center=�6I   $
��Left=�6m   $��162,316,0,0q�6�   $�� ��6�   $
 Wrap=�6�   $	��Top=�6�   $	��410��6�   $��24��6�   �� 5   
   -   5        $ DecryptOutput��5      $��Center=�6)   $��Center=�6L   $
��Left=�6p   $��162,349,0,0q�6�   $�� ��6�   $
 Wrap=�6�   $	��Top=�6�   $	��410��6�   $��24��6�   �� 5   
   -   5         ��Name$ 
EncryptBtn��5      $��Encrypt��6%   $
��Left=�67   $��
489,76,0,0q�6R   $	��Top=�6f   $��83��6~   $��27��6�   6�   �� 5   
   -   5        $ 
DecryptBtn��5      $��Decrypt��6%   $
��Left=�67   $��489,240,0,0q�6R   $	��Top=�6g   $��83��6   $��27��6�   6�   �� 5   
   -	   5        $ KeyTable��5      $��Center=�6%   $��Center=�6H   $
��Left=�6l   $��607,150,0,0q�6�   $�� ��6�   $
 Wrap=�6�   $	��Top=�6�   $	��140��6�   $	��178��6�   $��Lucida Console)�6�   $��23(�6  �� 5   
   $��
Key table:��6   $
��Left=�6%   $��621,106,0,0q�6@   $	��Top=�6U   $��24(�6m   �� 5   
   -
   5        $ ClearBtn��5      $��Clear fields��6#   $
��Left=�6:   $��607,349,0,0q�6U   $	��Top=�6j   $��24��6�   $	��140��6�   6�   5      5       