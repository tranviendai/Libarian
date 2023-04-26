import './App.scss';
import { GlobalContextProvider } from './contexts/GlobalContext';
import { BrowserRouter, Route, Routes } from 'react-router-dom';
import Layout from './layouts/PageLayout'
import Home from './pages/home';
import CategoryPage from './pages/category';
import BookPage from './pages/book';
import BookDetailPage from './pages/book-detail';
import PutBookPage from './pages/put-book';
import AboutPage from './pages/about';
import StaffPage from './pages/staff';

function App() {
  return <GlobalContextProvider>
    <BrowserRouter>
      <Layout>
        <Routes>
          <Route path='/' element={<Home />} />
          <Route path='/LMS' element={<Home />} />
          <Route path='/LMS/Category' element={<CategoryPage />} />
          <Route path='/LMS/Book' element={<BookPage />} />
          <Route path='/LMS/Book/:id' element={<BookDetailPage />} />
          <Route path='/LMS/AddBook' element={<PutBookPage />} />
          <Route path='/LMS/UpdateBook/:id' element={<PutBookPage />} />
          <Route path='/LMS/About/' element={<AboutPage />} />
          <Route path='/LMS/Staff/' element={<StaffPage />} />
        </Routes>
      </Layout>
    </BrowserRouter>
  </GlobalContextProvider>
}

export default App;

//TODO
