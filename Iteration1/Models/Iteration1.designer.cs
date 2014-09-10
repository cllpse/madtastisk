﻿#pragma warning disable 1591
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Iteration1
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.Data;
	using System.Collections.Generic;
	using System.Reflection;
	using System.Linq;
	using System.Linq.Expressions;
	
	
	[global::System.Data.Linq.Mapping.DatabaseAttribute(Name="Iteration1")]
	public partial class DataContext : System.Data.Linq.DataContext
	{
		
		private static System.Data.Linq.Mapping.MappingSource mappingSource = new AttributeMappingSource();
		
    #region Extensibility Method Definitions
    partial void OnCreated();
    partial void InsertIngredient(Iteration1.Entities.Ingredient instance);
    partial void UpdateIngredient(Iteration1.Entities.Ingredient instance);
    partial void DeleteIngredient(Iteration1.Entities.Ingredient instance);
    partial void InsertRecipe(Iteration1.Entities.Recipe instance);
    partial void UpdateRecipe(Iteration1.Entities.Recipe instance);
    partial void DeleteRecipe(Iteration1.Entities.Recipe instance);
    partial void InsertIngredientsRecipesRelation(Iteration1.Entities.IngredientsRecipesRelation instance);
    partial void UpdateIngredientsRecipesRelation(Iteration1.Entities.IngredientsRecipesRelation instance);
    partial void DeleteIngredientsRecipesRelation(Iteration1.Entities.IngredientsRecipesRelation instance);
    #endregion
		
		public DataContext() : 
				base(global::System.Configuration.ConfigurationManager.ConnectionStrings["Iteration1ConnectionString"].ConnectionString, mappingSource)
		{
			OnCreated();
		}
		
		public DataContext(string connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataContext(System.Data.IDbConnection connection) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataContext(string connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public DataContext(System.Data.IDbConnection connection, System.Data.Linq.Mapping.MappingSource mappingSource) : 
				base(connection, mappingSource)
		{
			OnCreated();
		}
		
		public System.Data.Linq.Table<Iteration1.Entities.Ingredient> Ingredients
		{
			get
			{
				return this.GetTable<Iteration1.Entities.Ingredient>();
			}
		}
		
		public System.Data.Linq.Table<Iteration1.Entities.Recipe> Recipes
		{
			get
			{
				return this.GetTable<Iteration1.Entities.Recipe>();
			}
		}
		
		public System.Data.Linq.Table<Iteration1.Entities.IngredientsRecipesRelation> IngredientsRecipesRelations
		{
			get
			{
				return this.GetTable<Iteration1.Entities.IngredientsRecipesRelation>();
			}
		}
	}
}
namespace Iteration1.Entities
{
	using System.Data.Linq;
	using System.Data.Linq.Mapping;
	using System.ComponentModel;
	using System;
	
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Ingredients")]
	public partial class Ingredient : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private string _Unit;
		
		private double _Amount;
		
		private EntitySet<IngredientsRecipesRelation> _IngredientsRecipesRelations;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnUnitChanging(string value);
    partial void OnUnitChanged();
    partial void OnAmountChanging(double value);
    partial void OnAmountChanged();
    #endregion
		
		public Ingredient()
		{
			this._IngredientsRecipesRelations = new EntitySet<IngredientsRecipesRelation>(new Action<IngredientsRecipesRelation>(this.attach_IngredientsRecipesRelations), new Action<IngredientsRecipesRelation>(this.detach_IngredientsRecipesRelations));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(256) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Unit", DbType="NVarChar(64) NOT NULL", CanBeNull=false)]
		public string Unit
		{
			get
			{
				return this._Unit;
			}
			set
			{
				if ((this._Unit != value))
				{
					this.OnUnitChanging(value);
					this.SendPropertyChanging();
					this._Unit = value;
					this.SendPropertyChanged("Unit");
					this.OnUnitChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Amount", DbType="Float NOT NULL")]
		public double Amount
		{
			get
			{
				return this._Amount;
			}
			set
			{
				if ((this._Amount != value))
				{
					this.OnAmountChanging(value);
					this.SendPropertyChanging();
					this._Amount = value;
					this.SendPropertyChanged("Amount");
					this.OnAmountChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Ingredient_IngredientsRecipesRelation", Storage="_IngredientsRecipesRelations", ThisKey="Id", OtherKey="IngredientId")]
		public EntitySet<IngredientsRecipesRelation> IngredientsRecipesRelations
		{
			get
			{
				return this._IngredientsRecipesRelations;
			}
			set
			{
				this._IngredientsRecipesRelations.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_IngredientsRecipesRelations(IngredientsRecipesRelation entity)
		{
			this.SendPropertyChanging();
			entity.Ingredient = this;
		}
		
		private void detach_IngredientsRecipesRelations(IngredientsRecipesRelation entity)
		{
			this.SendPropertyChanging();
			entity.Ingredient = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.Recipes")]
	public partial class Recipe : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private string _Name;
		
		private string _Description;
		
		private EntitySet<IngredientsRecipesRelation> _IngredientsRecipesRelations;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnNameChanging(string value);
    partial void OnNameChanged();
    partial void OnDescriptionChanging(string value);
    partial void OnDescriptionChanged();
    #endregion
		
		public Recipe()
		{
			this._IngredientsRecipesRelations = new EntitySet<IngredientsRecipesRelation>(new Action<IngredientsRecipesRelation>(this.attach_IngredientsRecipesRelations), new Action<IngredientsRecipesRelation>(this.detach_IngredientsRecipesRelations));
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Name", DbType="NVarChar(2569) NOT NULL", CanBeNull=false)]
		public string Name
		{
			get
			{
				return this._Name;
			}
			set
			{
				if ((this._Name != value))
				{
					this.OnNameChanging(value);
					this.SendPropertyChanging();
					this._Name = value;
					this.SendPropertyChanged("Name");
					this.OnNameChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Description", DbType="NVarChar(MAX)")]
		public string Description
		{
			get
			{
				return this._Description;
			}
			set
			{
				if ((this._Description != value))
				{
					this.OnDescriptionChanging(value);
					this.SendPropertyChanging();
					this._Description = value;
					this.SendPropertyChanged("Description");
					this.OnDescriptionChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Recipe_IngredientsRecipesRelation", Storage="_IngredientsRecipesRelations", ThisKey="Id", OtherKey="RecipeId")]
		public EntitySet<IngredientsRecipesRelation> IngredientsRecipesRelations
		{
			get
			{
				return this._IngredientsRecipesRelations;
			}
			set
			{
				this._IngredientsRecipesRelations.Assign(value);
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
		
		private void attach_IngredientsRecipesRelations(IngredientsRecipesRelation entity)
		{
			this.SendPropertyChanging();
			entity.Recipe = this;
		}
		
		private void detach_IngredientsRecipesRelations(IngredientsRecipesRelation entity)
		{
			this.SendPropertyChanging();
			entity.Recipe = null;
		}
	}
	
	[global::System.Data.Linq.Mapping.TableAttribute(Name="dbo.IngredientsRecipesRelations")]
	public partial class IngredientsRecipesRelation : INotifyPropertyChanging, INotifyPropertyChanged
	{
		
		private static PropertyChangingEventArgs emptyChangingEventArgs = new PropertyChangingEventArgs(String.Empty);
		
		private int _Id;
		
		private int _IngredientId;
		
		private int _RecipeId;
		
		private EntityRef<Ingredient> _Ingredient;
		
		private EntityRef<Recipe> _Recipe;
		
    #region Extensibility Method Definitions
    partial void OnLoaded();
    partial void OnValidate(System.Data.Linq.ChangeAction action);
    partial void OnCreated();
    partial void OnIdChanging(int value);
    partial void OnIdChanged();
    partial void OnIngredientIdChanging(int value);
    partial void OnIngredientIdChanged();
    partial void OnRecipeIdChanging(int value);
    partial void OnRecipeIdChanged();
    #endregion
		
		public IngredientsRecipesRelation()
		{
			this._Ingredient = default(EntityRef<Ingredient>);
			this._Recipe = default(EntityRef<Recipe>);
			OnCreated();
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_Id", AutoSync=AutoSync.OnInsert, DbType="Int NOT NULL IDENTITY", IsPrimaryKey=true, IsDbGenerated=true)]
		public int Id
		{
			get
			{
				return this._Id;
			}
			set
			{
				if ((this._Id != value))
				{
					this.OnIdChanging(value);
					this.SendPropertyChanging();
					this._Id = value;
					this.SendPropertyChanged("Id");
					this.OnIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_IngredientId", DbType="Int NOT NULL")]
		public int IngredientId
		{
			get
			{
				return this._IngredientId;
			}
			set
			{
				if ((this._IngredientId != value))
				{
					if (this._Ingredient.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnIngredientIdChanging(value);
					this.SendPropertyChanging();
					this._IngredientId = value;
					this.SendPropertyChanged("IngredientId");
					this.OnIngredientIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.ColumnAttribute(Storage="_RecipeId", DbType="Int NOT NULL")]
		public int RecipeId
		{
			get
			{
				return this._RecipeId;
			}
			set
			{
				if ((this._RecipeId != value))
				{
					if (this._Recipe.HasLoadedOrAssignedValue)
					{
						throw new System.Data.Linq.ForeignKeyReferenceAlreadyHasValueException();
					}
					this.OnRecipeIdChanging(value);
					this.SendPropertyChanging();
					this._RecipeId = value;
					this.SendPropertyChanged("RecipeId");
					this.OnRecipeIdChanged();
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Ingredient_IngredientsRecipesRelation", Storage="_Ingredient", ThisKey="IngredientId", OtherKey="Id", IsForeignKey=true)]
		public Ingredient Ingredient
		{
			get
			{
				return this._Ingredient.Entity;
			}
			set
			{
				Ingredient previousValue = this._Ingredient.Entity;
				if (((previousValue != value) 
							|| (this._Ingredient.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Ingredient.Entity = null;
						previousValue.IngredientsRecipesRelations.Remove(this);
					}
					this._Ingredient.Entity = value;
					if ((value != null))
					{
						value.IngredientsRecipesRelations.Add(this);
						this._IngredientId = value.Id;
					}
					else
					{
						this._IngredientId = default(int);
					}
					this.SendPropertyChanged("Ingredient");
				}
			}
		}
		
		[global::System.Data.Linq.Mapping.AssociationAttribute(Name="Recipe_IngredientsRecipesRelation", Storage="_Recipe", ThisKey="RecipeId", OtherKey="Id", IsForeignKey=true)]
		public Recipe Recipe
		{
			get
			{
				return this._Recipe.Entity;
			}
			set
			{
				Recipe previousValue = this._Recipe.Entity;
				if (((previousValue != value) 
							|| (this._Recipe.HasLoadedOrAssignedValue == false)))
				{
					this.SendPropertyChanging();
					if ((previousValue != null))
					{
						this._Recipe.Entity = null;
						previousValue.IngredientsRecipesRelations.Remove(this);
					}
					this._Recipe.Entity = value;
					if ((value != null))
					{
						value.IngredientsRecipesRelations.Add(this);
						this._RecipeId = value.Id;
					}
					else
					{
						this._RecipeId = default(int);
					}
					this.SendPropertyChanged("Recipe");
				}
			}
		}
		
		public event PropertyChangingEventHandler PropertyChanging;
		
		public event PropertyChangedEventHandler PropertyChanged;
		
		protected virtual void SendPropertyChanging()
		{
			if ((this.PropertyChanging != null))
			{
				this.PropertyChanging(this, emptyChangingEventArgs);
			}
		}
		
		protected virtual void SendPropertyChanged(String propertyName)
		{
			if ((this.PropertyChanged != null))
			{
				this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
			}
		}
	}
}
#pragma warning restore 1591
