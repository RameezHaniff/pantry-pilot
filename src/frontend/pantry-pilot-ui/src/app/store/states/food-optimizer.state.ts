import { Injectable } from '@angular/core';
import { MatSnackBar } from '@angular/material/snack-bar';
import { Action, Selector, State, StateContext } from '@ngxs/store';
import { OptimizationResponse } from '../../models/optimization-response';
import { FoodOptimizerGateway } from '../../services/food-optimizer.gateway';
import { FoodOptimizerActions } from '../actions/food-optimizer.actions';
import { tap, catchError, of } from 'rxjs';


export interface FoodOptimizerStateModel {
    loading: boolean;
    results: OptimizationResponse | null;
}

@State<FoodOptimizerStateModel>({
    name: 'foodOptimizer',
    defaults: {
        loading: false,
        results: null
    }
}
)

@Injectable()
export class FoodOptimizerState {

    constructor(
        private readonly gateway: FoodOptimizerGateway,
        private readonly snackbar: MatSnackBar
    ) { }

    @Selector()
    static getResults(state: FoodOptimizerStateModel): OptimizationResponse | null {
        return state.results;
    }

    @Selector()
    static getLoading(state: FoodOptimizerStateModel): boolean {
        return state.loading;
    }

    @Action(FoodOptimizerActions.Optimize)
    Optimize(ctx: StateContext<FoodOptimizerStateModel>, action: FoodOptimizerActions.Optimize) {
        ctx.patchState({ loading: true });

        return this.gateway.optimize(action.payload).pipe(
            tap(response => {
                ctx.dispatch(new FoodOptimizerActions.OptimizeSuccess(response));
            }),
            catchError(error => {
                ctx.dispatch(new FoodOptimizerActions.OptimizeFailure(error));
                return of(null);
            })
        );
    }

    @Action(FoodOptimizerActions.OptimizeSuccess)
    OptimizeSuccess(ctx: StateContext<FoodOptimizerStateModel>, action: FoodOptimizerActions.OptimizeSuccess) {
        ctx.patchState({ loading: false, results: action.payload });
    }

    @Action(FoodOptimizerActions.OptimizeFailure)
    OptimizeFailure(ctx: StateContext<FoodOptimizerStateModel>, action: FoodOptimizerActions.OptimizeFailure) {
        ctx.patchState({ loading: false });
        const message = action.payload?.error?.detail ?? 'An unexpected error occurred';

            this.snackbar.open(message, 'Dismiss',
            {
                duration: 5000,
                panelClass: ['error-snackbar']
            }
        );
    }

    @Action(FoodOptimizerActions.ResetOptimizationResult)
    ResetOptimizationResult(ctx: StateContext<FoodOptimizerStateModel>) {
        ctx.patchState({ results: null });
    }

}