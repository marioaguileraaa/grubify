import React, { useState, useEffect } from 'react';
import {
  Box,
  Typography,
  Card,
  CardMedia,
  CardContent,
  CardActions,
  Button,
  Chip,
  Rating,
  Container,
  CircularProgress,
  Alert,
  TextField,
  MenuItem,
  InputAdornment,
} from '@mui/material';
import {
  AccessTime as TimeIcon,
  LocalShipping as DeliveryIcon,
  Search as SearchIcon,
} from '@mui/icons-material';
import { useNavigate, useSearchParams } from 'react-router-dom';
import { Restaurant } from '../types';
import { restaurantService } from '../services/api';

const cuisineTypes = [
  'Todos',
  'Hombre',
  'Mujer',
  'Niños',
  'Accesorios',
  'Calzado',
  'Deportivo',
];

const HomePage: React.FC = () => {
  const navigate = useNavigate();
  const [searchParams] = useSearchParams();
  const [restaurants, setRestaurants] = useState<Restaurant[]>([]);
  const [filteredRestaurants, setFilteredRestaurants] = useState<Restaurant[]>([]);
  const [loading, setLoading] = useState(true);
  const [error, setError] = useState<string | null>(null);
  const [selectedCuisine, setSelectedCuisine] = useState('Todos');
  const [searchQuery, setSearchQuery] = useState(searchParams.get('search') || '');

  useEffect(() => {
    fetchRestaurants();
  }, []);

  useEffect(() => {
    filterRestaurants();
  }, [restaurants, selectedCuisine, searchQuery]);

  const fetchRestaurants = async () => {
    try {
      setLoading(true);
      const data = await restaurantService.getAll();
      setRestaurants(data);
      setError(null);
    } catch (err) {
      setError('Error al cargar los departamentos. Inténtalo de nuevo más tarde.');
      console.error('Error fetching restaurants:', err);
    } finally {
      setLoading(false);
    }
  };

  const filterRestaurants = () => {
    let filtered = restaurants;

    // Filter by cuisine
    if (selectedCuisine !== 'Todos') {
      filtered = filtered.filter(restaurant => 
        restaurant.cuisineType.toLowerCase() === selectedCuisine.toLowerCase()
      );
    }

    // Filter by search query
    if (searchQuery) {
      filtered = filtered.filter(restaurant =>
        restaurant.name.toLowerCase().includes(searchQuery.toLowerCase()) ||
        restaurant.cuisineType.toLowerCase().includes(searchQuery.toLowerCase()) ||
        restaurant.description.toLowerCase().includes(searchQuery.toLowerCase())
      );
    }

    setFilteredRestaurants(filtered);
  };

  const handleRestaurantClick = (restaurantId: number) => {
    navigate(`/restaurant/${restaurantId}`);
  };

  if (loading) {
    return (
      <Box display="flex" justifyContent="center" alignItems="center" minHeight="60vh">
        <CircularProgress size={60} />
      </Box>
    );
  }

  if (error) {
    return (
      <Container maxWidth="md">
        <Alert severity="error" sx={{ mt: 4 }}>
          {error}
        </Alert>
        <Box display="flex" justifyContent="center" mt={2}>
          <Button variant="contained" onClick={fetchRestaurants}>
            Try Again
          </Button>
        </Box>
      </Container>
    );
  }

  return (
    <Container maxWidth="xl">
      {/* Hero Section */}
      <Box
        sx={{
          backgroundImage: 'linear-gradient(135deg, #00704A 0%, #004D33 100%)',
          borderRadius: 3,
          color: 'white',
          p: 6,
          mb: 4,
          textAlign: 'center',
        }}
      >
        <Typography variant="h2" component="h1" gutterBottom>
          Moda y Estilo, Entrega Rápida
        </Typography>
        <Typography variant="h6" sx={{ mb: 4, opacity: 0.9 }}>
          Descubre las últimas tendencias y recíbelas en tu hogar
        </Typography>
        
        {/* Search Bar */}
        <Box maxWidth="600px" mx="auto">
          <TextField
            fullWidth
            variant="outlined"
            placeholder="Buscar departamentos, categorías o prendas..."
            value={searchQuery}
            onChange={(e) => setSearchQuery(e.target.value)}
            InputProps={{
              startAdornment: (
                <InputAdornment position="start">
                  <SearchIcon />
                </InputAdornment>
              ),
              sx: {
                backgroundColor: 'white',
                borderRadius: 2,
              },
            }}
          />
        </Box>
      </Box>

      {/* Cuisine Filter */}
      <Box sx={{ mb: 4 }}>
        <Typography variant="h5" gutterBottom>
          Explorar por Categoría
        </Typography>
        <Box sx={{ display: 'flex', gap: 1, flexWrap: 'wrap' }}>
          {cuisineTypes.map((cuisine) => (
            <Chip
              key={cuisine}
              label={cuisine}
              clickable
              variant={selectedCuisine === cuisine ? 'filled' : 'outlined'}
              color={selectedCuisine === cuisine ? 'primary' : 'default'}
              onClick={() => setSelectedCuisine(cuisine)}
              sx={{ mb: 1 }}
            />
          ))}
        </Box>
      </Box>

      {/* Results Header */}
      <Box sx={{ mb: 3 }}>
        <Typography variant="h5" gutterBottom>
          {searchQuery ? `Resultados de búsqueda para "${searchQuery}"` : 'Departamentos Destacados'}
        </Typography>
        <Typography variant="body1" color="text.secondary">
          {filteredRestaurants.length} departamento{filteredRestaurants.length !== 1 ? 's' : ''} encontrado{filteredRestaurants.length !== 1 ? 's' : ''}
        </Typography>
      </Box>

      {/* Restaurant Grid */}
      <Box
        sx={{
          display: 'grid',
          gridTemplateColumns: {
            xs: '1fr',
            sm: 'repeat(2, 1fr)',
            md: 'repeat(3, 1fr)',
            lg: 'repeat(4, 1fr)',
          },
          gap: 3,
        }}
      >
        {filteredRestaurants.map((restaurant) => (
          <Card
            key={restaurant.id}
            sx={{
              height: '100%',
              display: 'flex',
              flexDirection: 'column',
              cursor: 'pointer',
              transition: 'all 0.3s ease-in-out',
              '&:hover': {
                transform: 'translateY(-4px)',
                boxShadow: 4,
              },
            }}
            onClick={() => handleRestaurantClick(restaurant.id)}
          >
            <CardMedia
              component="img"
              height="200"
              image={restaurant.imageUrl}
              alt={restaurant.name}
              sx={{ objectFit: 'cover' }}
            />
            <CardContent sx={{ flexGrow: 1 }}>
              <Typography variant="h6" component="h2" gutterBottom>
                {restaurant.name}
              </Typography>
              <Typography variant="body2" color="text.secondary" sx={{ mb: 2 }}>
                {restaurant.description}
              </Typography>
              
              <Box sx={{ display: 'flex', alignItems: 'center', mb: 1 }}>
                <Rating value={restaurant.rating} precision={0.1} readOnly size="small" />
                <Typography variant="body2" sx={{ ml: 1 }}>
                  {restaurant.rating.toFixed(1)}
                </Typography>
              </Box>
              
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 2, mb: 1 }}>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <TimeIcon sx={{ fontSize: 16, mr: 0.5, color: 'text.secondary' }} />
                  <Typography variant="body2" color="text.secondary">
                    {restaurant.deliveryTime}
                  </Typography>
                </Box>
                <Box sx={{ display: 'flex', alignItems: 'center' }}>
                  <DeliveryIcon sx={{ fontSize: 16, mr: 0.5, color: 'text.secondary' }} />
                  <Typography variant="body2" color="text.secondary">
                    ${restaurant.deliveryFee.toFixed(2)}
                  </Typography>
                </Box>
              </Box>
              
              <Chip
                label={restaurant.cuisineType}
                size="small"
                variant="outlined"
                color="primary"
              />
            </CardContent>
            <CardActions sx={{ p: 2, pt: 0 }}>
              <Button
                fullWidth
                variant="contained"
                color="primary"
                onClick={(e) => {
                  e.stopPropagation();
                  handleRestaurantClick(restaurant.id);
                }}
              >
                Ver Colección
              </Button>
            </CardActions>
          </Card>
        ))}
      </Box>

      {filteredRestaurants.length === 0 && !loading && (
        <Box textAlign="center" py={8}>
          <Typography variant="h6" color="text.secondary" gutterBottom>
            No se encontraron departamentos
          </Typography>
          <Typography variant="body1" color="text.secondary">
            Intenta ajustar tu búsqueda o filtros
          </Typography>
        </Box>
      )}
    </Container>
  );
};

export default HomePage;
