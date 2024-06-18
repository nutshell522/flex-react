import { ReactNode } from "react";

const ProductFilter: React.FC<{ children: ReactNode }> = ({ children }) => {
    return (
        <div className="product-filter">
            <div className="product-filter__search">
                <input type="text" placeholder="Search..." />
            </div>
            {children}
        </div>
    )
}

export default ProductFilter;