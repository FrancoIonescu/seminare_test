import pandas as pd
import numpy as np
from pandas.api.types import is_numeric_dtype
from scipy.stats import entropy

def nan_replace(x):
    assert isinstance(x, pd.DataFrame)
    variabile = list(x.columns)
    for variabila in variabile:
        if any(x[variabila].isna()):
            if is_numeric_dtype(x[variabila]):
                x[variabila].fillna(x[variabila].mean(), inplace=True)
            else:
                modulul = x[variabila].mode()[0]
                x[variabila].fillna(modulul, inplace=True)

def diversitate_ (t, variabile):
    assert isinstance(t, pd.Series)
    x = t[variabile].values
    s = np.sum(x)
    p = x / s
    p[p == 0] = 1
    e = -np.sum(p * np.log2(p))
    s_s = np.sum(p*p)
    simpson = 1 - s_s
    inv_simpson = 1 / s_s
    s_div = pd.Series(
        data=[e, simpson, inv_simpson],
        index=["Shannon","Simpson","Inverse Simpson"]
    )
    return s_div

def diversitate(t):
    assert isinstance(t, pd.Series)
    x = np.array(t.iloc[:-1], dtype=float)
    s = np.sum(x)
    p = x / s
    p[p == 0] = 1
    e = -np.sum(p * np.log2(p))
    s_s = np.sum(p*p)
    simpson = 1 - s_s
    inv_simpson = 1 / s_s
    s_div = pd.Series(
        data=[t.iloc[len(t) - 1],e,simpson,inv_simpson],
        index=["Localitate","Shannon","Simpson","Inverse Simpson"]
    )
    return s_div
