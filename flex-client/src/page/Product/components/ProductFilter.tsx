import { useEffect } from "react";
import useProductAPI from "../../../hooks/productHook";
import { useLoaderContext } from "../../../contexts/LoderContext";
import Products from "./Products";

const ProductFilter: React.FC<{ searchStr: string }> = ({ searchStr }) => {

    const { products, setSearchParam, loading } = useProductAPI();
    const { setIsLoading } = useLoaderContext();

    useEffect(() => {
        if (!searchStr) return;
        const parsedCategoryId: number[] = searchStr.split('-').map((item) => Number(item)).filter((item) => !isNaN(item));
        setSearchParam({ topCategoryId: parsedCategoryId[0] ?? undefined, middleCategoryId: parsedCategoryId[1] ?? undefined, bottomCategoryId: parsedCategoryId[2] ?? undefined });
    }, [searchStr, setSearchParam]);


    useEffect(() => {
        setIsLoading(loading);
    }, [loading, setIsLoading]);

    return (
        <div className="product-filter">
            <Products products={products} />
        </div>
    )
}

export default ProductFilter;