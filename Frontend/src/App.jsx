import { useState } from 'react'
import { Router, Route, Routes } from "react-router-dom";
import './App.css'
import Header from './layouts/Header'
import LandingPage from './pages/LandingPage';
import Footer from './layouts/Footer';
import ManageAccount from './pages/ManageAccount';
import { BreadcrumbsDefault } from './layouts/BreadcrumbsDefault';

function App() {
  return (
    <>
     <Header/>
     <div className="pt-28">
      {/* <BreadcrumbsDefault/> */}
     <Routes>
          <Route path="/" element={<LandingPage />} />
          <Route path="/manage-account" element={<ManageAccount />} />
     </Routes>
     </div>
     <Footer/>
    </>
  )
}

export default App
