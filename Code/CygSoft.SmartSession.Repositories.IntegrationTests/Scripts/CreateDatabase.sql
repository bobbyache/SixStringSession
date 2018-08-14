USE [master]

IF EXISTS(select * from sys.databases where name='SmartSession_UnitTests')
	DROP DATABASE SmartSession_UnitTests


/****** Object:  Database [SmartSession_UnitTests]    Script Date: 2018/08/13 10:42:17 ******/
CREATE DATABASE [SmartSession_UnitTests]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SmartSession_UnitTests', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.ROBLT\MSSQL\DATA\SmartSession_UnitTests.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'SmartSession_UnitTests_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL13.ROBLT\MSSQL\DATA\SmartSession_UnitTests_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
