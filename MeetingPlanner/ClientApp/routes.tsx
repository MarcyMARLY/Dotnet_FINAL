import * as React from 'react';
import { Route } from 'react-router-dom';
import { Layout } from './components/Layout';
import { Home } from './components/Home';
import { FetchData } from './components/FetchData';
import { Counter } from './components/Counter';
import { EventLIst } from './components/EventLIst';
import { Busket } from './components/Busket';
import { History } from './components/History';

export const routes = <Layout>
	<Route exact path='/' component={EventLIst } />
    <Route path='/busket' component={ Busket } />
	<Route path='/history' component={History} />
	
</Layout>;
