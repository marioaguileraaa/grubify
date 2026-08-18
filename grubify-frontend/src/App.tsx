import React from 'react';
import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import { ThemeProvider, createTheme } from '@mui/material/styles';
import CssBaseline from '@mui/material/CssBaseline';
import { Container } from '@mui/material';

// Components
import Navbar from './components/Navbar';
import HomePage from './pages/HomePage';
import RestaurantPage from './pages/RestaurantPage';
import CartPage from './pages/CartPage';
import CheckoutPage from './pages/CheckoutPage';
import OrderTrackingPage from './pages/OrderTrackingPage';
import './App.css';

const theme = createTheme({
  palette: {
    primary: {
      main: '#00704A', // El Corte Inglés Green
      light: '#339769',
      dark: '#004D33',
    },
    secondary: {
      main: '#1A1A1A', // Black
      light: '#4A4A4A',
      dark: '#000000',
    },
    background: {
      default: '#F8F8F8',
      paper: '#FFFFFF',
    },
  },
  typography: {
    fontFamily: '"Playfair Display", "Georgia", "Times New Roman", serif',
    h1: {
      fontSize: '3rem',
      fontWeight: 700,
      letterSpacing: '-0.02em',
    },
    h2: {
      fontSize: '2.5rem',
      fontWeight: 600,
      letterSpacing: '-0.01em',
    },
    h3: {
      fontSize: '2rem',
      fontWeight: 600,
    },
    h4: {
      fontWeight: 600,
    },
    h5: {
      fontWeight: 600,
    },
    h6: {
      fontWeight: 600,
    },
    body1: {
      fontFamily: '"Inter", "Helvetica", "Arial", sans-serif',
    },
    body2: {
      fontFamily: '"Inter", "Helvetica", "Arial", sans-serif',
    },
    button: {
      fontFamily: '"Inter", "Helvetica", "Arial", sans-serif',
      textTransform: 'none' as const,
      fontWeight: 500,
    },
  },
  shape: {
    borderRadius: 8,
  },
});

function App() {
  return (
    <ThemeProvider theme={theme}>
      <CssBaseline />
      <Router>
        <div className="App">
          <Navbar />
          <Container maxWidth="xl" sx={{ mt: 3, mb: 3 }}>
            <Routes>
              <Route path="/" element={<HomePage />} />
              <Route path="/restaurant/:id" element={<RestaurantPage />} />
              <Route path="/cart" element={<CartPage />} />
              <Route path="/checkout" element={<CheckoutPage />} />
              <Route path="/order-tracking/:orderId" element={<OrderTrackingPage />} />
            </Routes>
          </Container>
        </div>
      </Router>
    </ThemeProvider>
  );
}

export default App;
