import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators, FormsModule } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { MatCardModule } from '@angular/material/card';
import { Observable } from 'rxjs';
import { Store } from '@ngxs/store';
import { IngredientResponse } from '../../models/ingredient-response';
import { IngredientsState } from '../../store/states/ingredients.state';
import { IngredientsActions } from '../../store/actions/ingredients.actions';
import { FoodOptimizerState } from '../../store/states/food-optimizer.state';
import { FoodOptimizerActions } from '../../store/actions/food-optimizer.actions';
import { IngredientQuantity, OptimizationRequest } from '../../models/optimization-request';
import { OptimizationResponse } from '../../models/optimization-response';
import { MatIconModule } from '@angular/material/icon';
import { MatTooltipModule } from '@angular/material/tooltip';

@Component({
  selector: 'app-optimizer',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    FormsModule,
    MatIconModule,
    MatFormFieldModule,
    MatSelectModule,
    MatInputModule,
    MatButtonModule,
    MatCardModule,
    MatTooltipModule
  ],
  templateUrl: './optimizer.component.html',
  styleUrl: './optimizer.component.scss'
})
export class OptimizerComponent implements OnInit {
  form!: FormGroup;
  ingredients$: Observable<IngredientResponse[]>;
  results$: Observable<OptimizationResponse | null>;
  selectedIngredients: IngredientQuantity[] = [];
  editingIndex: number | null = null;
  editingQuantity: number | null = null;

  constructor(
    private fb: FormBuilder,
    private store: Store
  ) {
    this.ingredients$ = this.store.select(IngredientsState.getIngredients);
    this.results$ = this.store.select(FoodOptimizerState.getResults);
  }

  ngOnInit() {
    this.form = this.fb.group({
      ingredientId: ['', Validators.required],
      quantity: ['', [Validators.required, Validators.min(0.1)]]
    });
    this.store.dispatch(new IngredientsActions.GetIngredients());
  }

  isIngredientSelected(ingredientId: number): boolean {
    return this.selectedIngredients.some(ing => ing.ingredientId === ingredientId);
  }

  addIngredient() {
    if (this.form.invalid) return;

    const { ingredientId, quantity } = this.form.value;
    const parsedId = parseInt(ingredientId);

    if (this.isIngredientSelected(parsedId)) return;

    this.selectedIngredients.push({
      ingredientId: parsedId,
      quantity: parseFloat(quantity)
    });

    this.form.reset();
  }

  removeIngredient(index: number) {
    this.selectedIngredients.splice(index, 1);
  }

  startEdit(index: number) {
    this.editingIndex = index;
    this.editingQuantity = this.selectedIngredients[index].quantity;
  }

  saveEdit(index: number) {
    if (this.editingQuantity !== null && this.editingQuantity > 0) {
      this.selectedIngredients[index].quantity = this.editingQuantity;
      this.editingIndex = null;
      this.editingQuantity = null;
    }
  }

  cancelEdit() {
    this.editingIndex = null;
    this.editingQuantity = null;
  }

  getIngredientName(ingredientId: number, ingredients: IngredientResponse[]): string {
    return ingredients.find(ing => ing.id === ingredientId)?.name || 'Unknown';
  }

  submitOptimization() {
    if (this.selectedIngredients.length === 0) return;

    const request: OptimizationRequest = {
      ingredients: this.selectedIngredients
    };

    this.store.dispatch(new FoodOptimizerActions.Optimize(request));
  }

  resetOptimization() {
    this.store.dispatch(new FoodOptimizerActions.ResetOptimizationResult());
  }
}
