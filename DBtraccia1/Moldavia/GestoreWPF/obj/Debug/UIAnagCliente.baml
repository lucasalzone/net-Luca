   M S B A M L   `   `   `  ����E  AGestoreWPF, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null3clr-namespace:GestionePraticheGestionePratiche  5   	   $    GestionePratiche.UiAnagCliente   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35D 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   0localclr-namespace:GestionePratiche     5   	     � Title$  AnagCliente��5   	   $	��450��6   $	��800��6*   .��5	      �� $��Center=�6   .+�5
   
   � .+�5      �� $
��Left=�6   $��10q�6/   $	��100��6;   .��6G   Nome:6O   �� 5      -   5        $��TxtNome��5      $	��100��6%   $��10q�61   6?   5      � 5   
   .+�5      �� $
��Left=�6   $��10q�6/   $	��100��6;   .��6G   
Cognome:6Q   �� 5      -   5        $��
TxtCognome��5      $	��100��6(   $��10q�64   6B   5      � 5   
   .+�5      �� $
��Left=�6   $��10q�6/   $	��100��6;   .��6G   Data di nascita:6Y   (  "System.Windows.Controls.DatePicker  5      -   5        $��
datePicker��5      $��10q�6+      SelectedDateFormat$
 Long=�5      �� 5      -   5        $��TxtDataNascita��5      ��6.   ��  �� ElementName$ 
datePicker�� �� Path$
 Text�5      � 5   
   .+�5      �� $
��Left=�6   $��10q�6/   $	��100��6;   .��6G   
Indirizzo:6S   �� 5      -   5        $��TxtIndirizzo��5      $	��200��6*   $��10q�66   6D   5      � 5   
   .+�5      �� $
��Left=�6   $��10q�6/   $	��100��6;   .��6G   Codice Cliente:6X   �� 5       -   5        $��TxtCodCliente��5       $	��100��6,   $��10q�68   6G   5!      � 5"   
   .+�5#      �� $
��Left=�6   $��10q�6/   $	��100��6;   .��6G   Ricerca Universale:6\   �� 5$      -   5        $��TxtRicUniversale��5$      $	��200��6/   $��10q�6;   6J   5%      � 5&   
   $��Center=�6   .+�5'      �� -   5        $��	BtnAvanti��5'      {�.  $��10q�6P   .��6\   AVANTI6d   �� 5(      -	   5        $��BtnIndietro��5(      {�.  $��10q�6T   .��6`   
INDIETRO6j   �� 5)      -
   5        $��BtnAggiungi��5)      $��10q�6B   .��6N   
AGGIUNGI6X   �� 5*      -   5        $��BtnModifica��5*      $��10q�6B   .��6N   
MODIFICA6X   �� 5+      -   5        $��BtnCerca��5+      $��10q�6<   .��6H   CERCA6O   �� 5,      -   5        $��BtnClearCampi��5,      $��10q�6F   .��6R   Clear6Y   5-      5.      5/      