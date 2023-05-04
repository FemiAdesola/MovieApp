import React from 'react';
import Header from '../Header';
import { Outlet } from 'react-router-dom';
import Footer from '../Footer';
import FooterBottom from '../FooterBottom';

const Layout = () => {
    return (
      <div>
        <Header />
        <div className="container">
          <Outlet />
        </div>
        <Footer />
        <div id="footer">{ <FooterBottom />}</div>
      </div>
    );
};

export default Layout;