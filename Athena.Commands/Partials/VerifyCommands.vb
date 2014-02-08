Imports Athena.Analysis
Imports Athena.Core
Imports Leviathan.Commands
Imports Leviathan.Commands.StringCommands
Imports Leviathan.Configuration
Imports Leviathan.Visualisation
Imports IT = Leviathan.Visualisation.InformationType
Imports VL = Leviathan.Commands.VerbosityLevel
Imports System.Collections.Generic
Imports System.Data
Imports System.Data.SqlClient
Imports System.Linq
Imports System.Text
Imports System.Xml
Imports C = System.Data.CommandType

Partial Public Class VerifyCommands

	#Region " Public Command Methods "

		<Command( _
			Name:="batches", _
			Description:="Verify Batches against Hermes Logs" _
		)> _
		Public Function Verify_Batches( _
			<Configurable( _
				Description:="Source Athena XML Document" _
			)> _
			ByVal source_Document As String, _
			<Configurable( _
				Description:="Output Athena XML Document" _
			)> _
			ByVal output_Document As String, _
			<Configurable( _
				Description:="Hermes Email Log Directory" _
			)> _
			ByVal log_Directory As IO.DirectoryInfo _
		) As IFixedWidthWriteable

			Dim summary_Rows As New List(Of Row)

			Dim source_Doc As New XmlDocument()
			If Not System.io.File.Exists(source_Document) Then
				Host.Warn(String.Format("Cannot find Source File ({0})", source_Document))
				Return Nothing
			End If
			source_Doc.Load(source_Document)

			Dim exercise_Name As String = source_Doc.SelectSingleNode("//" & _
				CreationCommands.XML_ELEMENT_EXERCISE).Attributes.ItemOf(CreationCommands.XML_ATTRIBUTE_NAME).Value

			Dim success_Addresses As New List(Of String)
			Dim failed_Addresses As New List(Of String)
			Dim processed_Logs As Int32
			Dim last_Failed_Count As Int32 = 0
			Dim processed_Instances As New Dictionary(Of Int32, Int32)
			Dim removed_Instances As New Dictionary(Of Int32, Int32)
			Dim kept_Instances As New Dictionary(Of Int32, Int32)

			Dim all_Files As System.IO.FileInfo() = log_Directory.GetFiles("*.xml", System.IO.SearchOption.TopDirectoryOnly)

			For i As Integer = 0 To all_Files.Length - 1

				Dim xr As New XmlTextReader(all_Files(i).FullName)

				If Not Hermes.Email.Manager.LoadLogFile(xr, success_Addresses, failed_Addresses) Then

					Host.Warn(String.Format("Failed to Load/Decode Hermes Log File ({0})", all_Files(i).FullName))

				Else

					If failed_Addresses.Count > last_Failed_Count Then

						For j As Integer = last_Failed_Count To failed_Addresses.Count - 1
							Host.Warn(String.Format("Failed to Send to {0} in Log File ({1})", failed_Addresses(j), all_Files(i).FullName))
						Next
						last_Failed_Count = failed_Addresses.Count

					End If
					processed_Logs += 1

				End If

				xr.Close()
				xr = Nothing

				Host.Progress((i + 1) / all_Files.Length, "Parsing Hermes Log Files")

			Next

			Dim all_Batches As XmlNodeList = source_Doc.SelectNodes("descendant::" & _
				CreationCommands.XML_ELEMENT_BATCH)

			For i As Integer = 0 To all_Batches.Count - 1

				Dim batch_No As Integer = i + 1

				processed_Instances.Add(batch_No, 0)
				removed_Instances.Add(batch_No, 0)
				kept_Instances.Add(batch_No, 0)

				Dim all_Instances As XmlNodeList = all_Batches(i).SelectNodes("descendant::" & _
					CreationCommands.XML_ELEMENT_INSTANCE)

				Dim remove_Instances As New List(Of XmlNode)

				For j As Integer = 0 To all_Instances.Count - 1

					Dim all_Addresses As XmlNodeList = all_Instances(j).SelectNodes("descendant::" & _
						CreationCommands.XML_ELEMENT_TO)

					Dim remove_Instance As Boolean = True
					Dim remove_Addresses As New List(Of XmlNode)

					For k As Integer = 0 To all_Addresses.Count - 1

						If Not String.IsNullOrEmpty(all_Addresses(k).InnerText) AndAlso Not success_Addresses.Contains(all_Addresses(k).InnerText) Then
							remove_Instance = False
						Else
							all_Addresses(k).RemoveAll()
							remove_Addresses.Add(all_Addresses(k))
						End If

					Next

					For Each remove_Node As XmlNode In remove_Addresses
						remove_Node.ParentNode.RemoveChild(remove_Node)
					Next

					If remove_Instance Then
						removed_Instances(batch_No) += 1
						all_Instances(j).RemoveAll()
						remove_Instances.Add(all_Instances(j))
					Else
						kept_Instances(batch_No) += 1
					End If
					processed_Instances(batch_No) += 1

					Host.Progress((j + 1) / all_Instances.Count, "Checking Athena Batch " & CStr(batch_No))

				Next

				For Each remove_Node As XmlNode In remove_Instances
					remove_Node.ParentNode.RemoveChild(remove_Node)
				Next

				summary_Rows.Add(New Row().Add("Batch " & batch_No & ": Instances").Add(processed_Instances(batch_No)))
				summary_Rows.Add(New Row().Add("Batch " & batch_No & ": Removed").Add(removed_Instances(batch_No)))
				summary_Rows.Add(New Row().Add("Batch " & batch_No & ": Kept").Add(kept_Instances(batch_No)))

			Next

			summary_Rows.Insert(0, New Row().Add("Batch Count").Add(all_Batches.Count))
			summary_Rows.Insert(0, New Row().Add("Log Files").Add(processed_Logs))
			summary_Rows.Insert(0, New Row().Add("Successful Addresses").Add(success_Addresses.Count))
			summary_Rows.Insert(0, New Row().Add("Failed Addresses").Add(failed_Addresses.Count))
			summary_Rows.Insert(0, New Row().Add("Exercise").Add(exercise_Name))
			
			If System.IO.File.Exists(output_Document) Then System.IO.File.Delete(output_Document)

			Dim output_Writer As New System.Xml.XmlTextWriter(output_Document, System.Text.Encoding.UTF8)
			output_Writer.Formatting = System.Xml.Formatting.Indented
			output_Writer.IndentChar = Microsoft.VisualBasic.ChrW(&H9)

			source_Doc.Save(output_Writer)
			output_Writer.Close()
			output_Writer = Nothing

			Return Cube.Create(IT.Information, "Exercises // Summary", "Name", "Value").Add(New Slice(summary_Rows))

		End Function


	#End Region

End Class