import { ComponentFixture, TestBed } from '@angular/core/testing';
import { of } from 'rxjs';
import { FormBuilder } from '@angular/forms';
import { Store } from '@ngxs/store';

import { OptimizerComponent } from './optimizer.component';
import { IngredientsActions } from '../../store/actions/ingredients.actions';
import { FoodOptimizerActions } from '../../store/actions/food-optimizer.actions';

describe('OptimizerComponent', () => {
  let component: OptimizerComponent;
  let fixture: ComponentFixture<OptimizerComponent>;
  let store: jasmine.SpyObj<Store>;

  beforeEach(async () => {
    store = jasmine.createSpyObj('Store', [
      'dispatch',
      'select'
    ]);

    store.select.and.returnValue(of([]));

    await TestBed.configureTestingModule({
      imports: [OptimizerComponent],
      providers: [
        FormBuilder,
        {
          provide: Store,
          useValue: store
        }
      ]
    }).compileComponents();

    fixture = TestBed.createComponent(OptimizerComponent);
    component = fixture.componentInstance;

    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });

  it('should dispatch GetIngredients on init', () => {
    expect(store.dispatch).toHaveBeenCalledWith(
      new IngredientsActions.GetIngredients()
    );
  });

  it('should initialize form', () => {
    expect(component.form).toBeTruthy();

    expect(component.form.controls['ingredientId']).toBeTruthy();

    expect(component.form.controls['quantity']).toBeTruthy();
  });

  it('should not add ingredient when form is invalid', () => {
    component.addIngredient();

    expect(component.selectedIngredients.length).toBe(0);
  });

  it('should add ingredient when form is valid', () => {
    component.form.patchValue({
      ingredientId: '1',
      quantity: 5
    });

    component.addIngredient();

    expect(component.selectedIngredients.length).toBe(1);

    expect(component.selectedIngredients[0]).toEqual({
      ingredientId: 1,
      quantity: 5
    });
  });

  it('should not add duplicate ingredients', () => {
    component.selectedIngredients = [
      {
        ingredientId: 1,
        quantity: 5
      }
    ];

    component.form.patchValue({
      ingredientId: '1',
      quantity: 10
    });

    component.addIngredient();

    expect(component.selectedIngredients.length).toBe(1);
  });

  it('should remove ingredient', () => {
    component.selectedIngredients = [
      {
        ingredientId: 1,
        quantity: 5
      }
    ];

    component.removeIngredient(0);

    expect(component.selectedIngredients.length).toBe(0);
  });

  it('should identify selected ingredient', () => {
    component.selectedIngredients = [
      {
        ingredientId: 1,
        quantity: 5
      }
    ];

    expect(component.isIngredientSelected(1)).toBeTrue();

    expect(component.isIngredientSelected(2)).toBeFalse();
  });

  it('should start editing ingredient', () => {
    component.selectedIngredients = [
      {
        ingredientId: 1,
        quantity: 5
      }
    ];

    component.startEdit(0);

    expect(component.editingIndex).toBe(0);
    expect(component.editingQuantity).toBe(5);
  });

  it('should save edited quantity', () => {
    component.selectedIngredients = [
      {
        ingredientId: 1,
        quantity: 5
      }
    ];

    component.editingQuantity = 10;

    component.saveEdit(0);

    expect(component.selectedIngredients[0].quantity)
      .toBe(10);

    expect(component.editingIndex)
      .toBeNull();

    expect(component.editingQuantity)
      .toBeNull();
  });

  it('should cancel edit', () => {
    component.editingIndex = 0;
    component.editingQuantity = 10;

    component.cancelEdit();

    expect(component.editingIndex).toBeNull();
    expect(component.editingQuantity).toBeNull();
  });

  it('should return ingredient name', () => {
    const ingredients = [
      {
        id: 1,
        name: 'Cheese'
      }
    ];

    const result = component.getIngredientName(
      1,
      ingredients
    );

    expect(result).toBe('Cheese');
  });

  it('should return Unknown when ingredient not found', () => {
    const result = component.getIngredientName(
      999,
      []
    );

    expect(result).toBe('Unknown');
  });

  it('should dispatch optimize action', () => {
    component.selectedIngredients = [
      {
        ingredientId: 1,
        quantity: 5
      }
    ];

    component.submitOptimization();

    expect(store.dispatch).toHaveBeenCalledWith(
      jasmine.any(FoodOptimizerActions.Optimize)
    );
  });

  it('should not dispatch optimize when no ingredients selected', () => {
    store.dispatch.calls.reset();

    component.selectedIngredients = [];

    component.submitOptimization();

    expect(store.dispatch).not.toHaveBeenCalled();
  });

  it('should dispatch reset action', () => {
    component.resetOptimization();

    expect(store.dispatch).toHaveBeenCalledWith(
      new FoodOptimizerActions.ResetOptimizationResult()
    );
  });
});