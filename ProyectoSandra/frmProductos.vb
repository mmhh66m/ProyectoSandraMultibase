﻿
Imports System.ComponentModel
Imports System.Data.SqlClient
Public Class frmProductos
    Dim conexion As New SqlConnection
    Dim comando As New SqlCommand
    Dim lector As SqlDataReader
    Dim opcion As Integer = 0
    Private Sub frmSucursales_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        conexion = New SqlConnection(sqlServerC)
        conexion.Open()
        comando = conexion.CreateCommand
        comando.CommandText = "Select * from Productos"
        lector = comando.ExecuteReader
        While lector.Read()
            DataGridView1.Rows.Add(lector(0), lector(1))
        End While
        lector.Close()
        colocar(0)
    End Sub
    Private Sub btnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        opcion = 1
        Dim filas As Integer
        filas = DataGridView1.RowCount
        filas -= 1
        colocar(filas)
        comando.CommandText = "SELECT count(idProducto) FROM Productos"
        lector = comando.ExecuteReader
        lector.Read()
        txtIdProducto.Text = CInt(lector(0)) + 1
        lector.Close()
        activar()
    End Sub
    Private Sub btnGrabar_Click(sender As Object, e As EventArgs) Handles btnguardar.Click
        Try
            If opcion = 2 Then
                comando.CommandText = "UPDATE Productos set nombre = '" & txtNombre.Text & "' Where idProducto =" & txtIdProducto.Text
            ElseIf opcion = 1 Then
                comando.CommandText = "insert into Productos(idProducto,nombre) values(" & txtIdProducto.Text & ",'" & txtNombre.Text & "')"
            End If
            comando.ExecuteNonQuery()
        Catch ex As Exception
            MsgBox("Ocurrio un error al intentar grabar compruebe los datos")
        End Try
        actualizar()
        GroupBox1.Enabled = False
        GroupBox2.Enabled = True
        btnguardar.Enabled = False
        btnModificar.Enabled = True
        btnAgregar.Enabled = True
        btnSalir.Enabled = True
    End Sub
    Private Sub btnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        Dim fila As Integer
        fila = DataGridView1.CurrentRow.Index
        fila += 1
        Dim filas As Integer
        filas = DataGridView1.RowCount
        If fila < filas - 1 Then
            colocar(fila)
        End If
    End Sub

    Private Sub btnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        Dim fila As Integer
        fila = DataGridView1.CurrentRow.Index
        fila -= 1
        If fila < 0 Then
            fila = 0
        End If
        colocar(fila)
    End Sub
    Private Sub btnModificar_Click(sender As Object, e As EventArgs) Handles btnModificar.Click
        activar()
        opcion = 2
    End Sub

    Private Sub btnInicio_Click(sender As Object, e As EventArgs) Handles btnInicio.Click
        colocar(0)
    End Sub

    Private Sub btnUltimo_Click(sender As Object, e As EventArgs) Handles btnUltimo.Click
        Dim filas As Integer
        filas = DataGridView1.RowCount
        filas -= 2
        If filas < 1 Then
            filas = 0
        End If
        colocar(filas)
    End Sub

    Private Sub btnSalir_Click(sender As Object, e As EventArgs) Handles btnSalir.Click
        conexion.Close()
        Dispose()
    End Sub
    Private Sub actualizar()
        DataGridView1.Rows.Clear()
        comando.CommandText = "Select * from Productos"
        lector = comando.ExecuteReader
        While lector.Read()
            DataGridView1.Rows.Add(lector(0), lector(1))
        End While
        lector.Close()
    End Sub
    Private Sub activar()
        GroupBox1.Enabled = True
        GroupBox3.Enabled = False
        btnAgregar.Enabled = False
        btnModificar.Enabled = False
        btnSalir.Enabled = False
        btnguardar.Enabled = True
    End Sub
    Private Sub colocar(fila As Integer)
        DataGridView1.CurrentCell = DataGridView1(0, fila)
        txtIdProducto.Text = DataGridView1.Item(0, fila).Value
        txtNombre.Text = DataGridView1.Item(1, fila).Value
    End Sub

    Private Sub frmProductos_Closing(sender As Object, e As CancelEventArgs) Handles Me.Closing
        conexion.Close()
    End Sub
End Class