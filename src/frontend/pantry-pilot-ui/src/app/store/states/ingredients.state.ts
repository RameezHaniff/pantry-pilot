import { Injectable } from '@angular/core';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { IngredientsActions } from '../actions/ingredients.actions';
import { IngredientResponse } from '../../models/ingredient-response';
import { IngredientsGateway } from '../../services/ingredients.gateway';
import { tap, catchError, of } from 'rxjs';


export interface IngredientsStateModel {
    loading: boolean;
    ingredients: IngredientResponse[];
}

@State<IngredientsStateModel>({
  name: 'ingredients',
  defaults: {
    loading: false,
    ingredients: []
  }}
)

@Injectable()
export class IngredientsState {

    constructor(private readonly gateway: IngredientsGateway) { }

    @Selector()
    static getIngredients(state: IngredientsStateModel) {
        return state.ingredients;
    }

    @Action(IngredientsActions.GetIngredients)
    getIngredients(ctx: StateContext<IngredientsStateModel>) {
        ctx.patchState({ loading: true });
        return this.gateway.getIngredients().pipe(
            tap(response => {
                ctx.dispatch(new IngredientsActions.GetIngredientsSuccess(response));
            }),
            catchError(error => {
                ctx.dispatch(new IngredientsActions.GetIngredientsFailure(error));
                return of(null);
            })
        );
        
    }

    @Action(IngredientsActions.GetIngredientsSuccess)
    getIngredientsSuccess(ctx: StateContext<IngredientsStateModel>, action: IngredientsActions.GetIngredientsSuccess) {
        ctx.patchState({ loading: false, ingredients: action.payload });
    }

    @Action(IngredientsActions.GetIngredientsFailure)
    getIngredientsFailure(ctx: StateContext<IngredientsStateModel>, action: IngredientsActions.GetIngredientsFailure) {
        ctx.patchState({ loading: false });
    }
    
}